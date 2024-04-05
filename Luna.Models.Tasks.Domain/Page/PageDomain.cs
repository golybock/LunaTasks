using Luna.Models.Tasks.Database.Card;
using Luna.Models.Tasks.Database.Page;
using Luna.Models.Tasks.Domain.Card;
using Luna.Models.Users.Domain.Users;
using Luna.Models.Users.View.Users;

namespace Luna.Models.Tasks.Domain.Page;

public class PageDomain
{
	public Guid Id { get; set; }

	public String Name { get; set; } = null!;

	public String Description { get; set; } = null!;

	public String HeaderImage { get; set; } = null!;

	public DateTime CreatedTimestamp { get; set; }

	public Guid CreatedUserId { get; set; }

	public UserDomain CreatedUser { get; set; }

	public PageDomain(PageDatabase pageDatabase, UserDomain userDomain)
	{
		Id = pageDatabase.Id;
		Name = pageDatabase.Name;
		Description = pageDatabase.Description;
		HeaderImage = pageDatabase.HeaderImage;
		CreatedTimestamp = pageDatabase.CreatedTimestamp;
		CreatedUserId = pageDatabase.CreatedUserId;
		CreatedUser = userDomain;
	}
}