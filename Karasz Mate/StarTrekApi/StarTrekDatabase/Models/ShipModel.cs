using Microsoft.IdentityModel.Tokens;
using StarTrekDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StarTrekDatabase.Models
{
    public class ShipModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Class { get; set; }

        public string RaceFaction { get; set; }

        public int Length { get; set; }

        public int Crew { get; set; }

        public double MaxWarp { get; set; }

        public string Armament { get; set; }

        public string ShieldType { get; set; }

        public string HullMaterial { get; set; }

        public string Role { get; set; }



        public ShipModel() { }

        public ShipModel(ShipEntity entity)
        {
            if (entity is null)
            {
                return;
            }
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Class = entity.Class;
            this.RaceFaction = entity.RaceFaction;
            this.Length = entity.Length;
            this.Crew = entity.Crew;
            this.MaxWarp = entity.MaxWarp;
            this.Armament = entity.Armament;
            this.ShieldType = entity.ShieldType;
            this.HullMaterial = entity.HullMaterial;
            this.Role = entity.Role;
            
        }


        public ShipEntity ToEntity()
        {
            return new ShipEntity
            {
                Id = Id,
                Name = Name,
                Class = Class,
                RaceFaction = RaceFaction,
                Length = Length,
                Crew = Crew,
                MaxWarp = MaxWarp,
                Armament = Armament,
                ShieldType = ShieldType,
                HullMaterial = HullMaterial,
                Role = Role,
            };
        }

        public void ToEntity(ShipEntity entity)
        {
            entity.Id = Id;
            entity.Name = Name;
            entity.Class = Class;
            entity.RaceFaction = RaceFaction;
            entity.Length = Length;
            entity.Crew = Crew;
            entity.MaxWarp = MaxWarp;
            entity.Armament = Armament;
            entity.ShieldType = ShieldType;
            entity.HullMaterial = HullMaterial;
            entity.Role = Role;
                
        }
   
    }
}
