#nullable disable
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace mAPI.UiTests.Database
{
    public class DCandidate : IDbEntity
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string BloodGroup { get; set; }

        public string Address { get; set; }


        public string GetId()
        {
            return Id.ToString();
        }

        internal class DCandidateEntityConfiguration : IEntityTypeConfiguration<DCandidate>
        {
            public void Configure(EntityTypeBuilder<DCandidate> builder)
            {
                builder.ToTable("DCandidates", "dbo");

                builder.HasKey(e => e.Id);
            }
        }
    }
}
