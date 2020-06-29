using BaseCleanArchitecture.Core.Shared.Classes;

namespace BaseCleanArchitecture.Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
