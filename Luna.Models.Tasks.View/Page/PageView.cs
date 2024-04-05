using Luna.Models.Tasks.Domain.Page;
using Luna.Models.Tasks.View.Card;
using Luna.Models.Users.View.Users;

namespace Luna.Models.Tasks.View.Page;

public class PageView
{
	public Guid Id { get; set; }

	public String Name { get; set; }

	public String Description { get; set; }

	public String HeaderImage { get; set; }

	public DateTime CreatedTimestamp { get; set; }

	public UserView CreatedUser { get; set; }

	public PageView(PageDomain pageDomain)
	{
		Id = pageDomain.Id;
		Name = pageDomain.Name;
		Description = pageDomain.Description;
		HeaderImage = pageDomain.HeaderImage;
		CreatedTimestamp = pageDomain.CreatedTimestamp;
		CreatedUser = new UserView(pageDomain.CreatedUser);
	}
}