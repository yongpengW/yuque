using Microsoft.EntityFrameworkCore;
using Yuque.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.EFCore.Mapping
{
    public interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }

    public interface IMappingConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>, IMappingConfiguration where TEntity : class, IEntity
    {

    }
}
