using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Infrastructure.Configurations;

public class MessageConfigurations : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();   

        builder.Property(x => x.Content)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.SentAt)
            .IsRequired();

        builder.HasOne(x => x.Sender);

        builder.HasOne(x => x.Group)
            .WithMany(x => x.Messages);

    }
}
