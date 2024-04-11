using Luna.Models.Notification.Database.Notifications;
using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;

namespace Luna.Notification.Repositories.Repositories;

public class NotificationRepository: NpgsqlRepository, INotificationRepository
{
	public NotificationRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }


	public async Task<NotificationDatabase?> GetNotificationByIdAsync(Guid id)
	{
		var query = "select * from notification where id = $1";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = id}
		};

		return await GetAsync<NotificationDatabase>(query, parameters);
	}

	public async Task<IEnumerable<NotificationDatabase>> GetNotificationsByIdsAsync(IEnumerable<Guid> ids)
	{
		var query = "select * from notification where id = any ($1)";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = ids.ToList()}
		};

		return await GetListAsync<NotificationDatabase>(query, parameters);
	}

	public async Task<IEnumerable<NotificationDatabase>> GetNotificationsAsync(Guid userId, bool withRead = false)
	{
		// todo fix
		if (withRead)
		{
			var query = "select * from notification where user_id = $1";

			var parameters = new[]
			{
				new NpgsqlParameter() {Value = userId},
			};

			return await GetListAsync<NotificationDatabase>(query, parameters);
		}
		else
		{
			var query = "select * from notification where user_id = $1 and read = false";

			var parameters = new[]
			{
				new NpgsqlParameter() {Value = userId}
			};

			return await GetListAsync<NotificationDatabase>(query, parameters);
		}
	}

	public async Task<IEnumerable<NotificationDatabase>> GetNotificationsByCreatorAsync(Guid createdUserId)
	{
		var query = "select * from notification where created_user = $1";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = createdUserId}
		};

		return await GetListAsync<NotificationDatabase>(query, parameters);
	}

	public async Task<bool> CreateNotificationAsync(NotificationDatabase notificationDatabase)
	{
		var query = "insert into notification(id, user_id, text, created_user, image_url) values ($1, $2, $3, $4, $5)";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = notificationDatabase.Id},
			new NpgsqlParameter() {Value = notificationDatabase.UserId},
			new NpgsqlParameter() {Value = notificationDatabase.Text},
			new NpgsqlParameter() {Value = notificationDatabase.CreatedUser},
			new NpgsqlParameter() {Value = notificationDatabase.ImageUrl},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> UpdateNotificationAsync(Guid id, NotificationDatabase notificationDatabase)
	{
		var query = "update notification set text = $2, image_url = $3, read = $4, read_at = $5, priority = $6 where id = $1";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = id},
			new NpgsqlParameter() {Value = notificationDatabase.Text},
			new NpgsqlParameter() {Value = notificationDatabase.ImageUrl},
			new NpgsqlParameter() {Value = notificationDatabase.Read},
			new NpgsqlParameter() {Value = notificationDatabase.ReadAt},
			new NpgsqlParameter() {Value = notificationDatabase.Priority},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> ReadNotificationsAsync(Guid id)
	{
		var query = "update notification set read = true, read_at = $2 where id = $1";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = id},
			new NpgsqlParameter() {Value = DateTime.UtcNow},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> UnReadNotificationsAsync(Guid id)
	{
		var query = "update notification set read = false, read_at = null where id = $1";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = id},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> ReadNotificationsAsync(IEnumerable<Guid> ids)
	{
		var query = "update notification set read = true, read_at = $2 where id = any ($1)";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = ids.ToList()},
			new NpgsqlParameter() {Value = DateTime.UtcNow},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> UnReadNotificationsAsync(IEnumerable<Guid> ids)
	{
		var query = "update notification set read = false, read_at = null where id = any ($1)";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = ids.ToList()},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> DeleteNotificationAsync(Guid id)
	{
		return await DeleteAsync("notification", "id", id);
	}

	public async Task<bool> DeleteUserAsync(Guid userId)
	{
		return await DeleteAsync("notification", "user_id", userId);
	}
}