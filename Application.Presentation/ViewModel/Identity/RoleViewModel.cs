namespace Application.Presentation.ViewModel.Identity
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<UserRoleViewModel> User { get; set; } = new List<UserRoleViewModel>();
        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
