using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using buildingWebApi.Models;

namespace buildingWebApi.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions):base(dbContextOptions){}

        public DbSet<BuildingEntity> Buildings {get; set;}

        public DbSet<BuildingImageEntity> buildingImages{get; set;}
        public DbSet<CommunicationEntity> Communications{get; set;}

        public DbSet<RoleEntity> Roles {get; set;}
        public DbSet<UserEntity> Users{get; set;}

        public DbSet<RentAreaEntity> RentAreas{get; set;}

        public DbSet<PaymentEntity> Payments{get; set;}   

        public DbSet<LoveEntity> Loves {get; set;}    
        


        public override int SaveChanges(){
            foreach(var entry in ChangeTracker.Entries<BaseEntity>()){
                if(entry.State == EntityState.Added){
                    entry.Entity.OnInsert();
                }
                if(entry.State == EntityState.Modified){
                    entry.Entity.OnUpdate();
                }
            }
            return base.SaveChanges();
        }
          public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.OnInsert();
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.OnUpdate();
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    }
} 