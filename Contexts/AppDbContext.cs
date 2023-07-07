using ApiRVM2019.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        //especificacion tablas
        public DbSet<Reclamo>Reclamo{ get; set; }
        public DbSet<TipoEstado>TipoEstado{ get; set; }
        public DbSet<Estado>Estado{ get; set; }
        public DbSet<Sesion> Sesion { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<TipoReclamo> TipoReclamo { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<DetalleReclamo> DetalleReclamo { get; set;}
        public DbSet<ReclamoAmbiental> ReclamoAmbiental { get; set; }
        public DbSet<VehiculoXDetalleReclamo> VehiculoXDetalleReclamo { get; set; }
        public DbSet<Vehiculo> Vehiculo { get; set; }
        public DbSet<MarcaVehiculo> MarcaVehiculo { get; set; }
        public DbSet<ModeloVehiculo> ModeloVehiculo { get; set; }
        public DbSet<TipoVehiculo> TipoVehiculo { get; set; }
        

        //Dashboard
        public DbSet<V_CantidadxEstado> V_CantidadxEstado {get; set;}
        public DbSet<Entities.Dashboard.V_TotalReclamosRealizados> V_TotalReclamosRealizados { get; set; }
        public DbSet<Entities.Dashboard.V_CantidadxEstadoUsuario> V_CantidadxEstadoUsuario { get; set; }
        public DbSet<Entities.Dashboard.V_CantidadTipReclamoUsuario> V_CantidadTipReclamoUsuario { get; set;}
        public DbSet<Entities.Dashboard.V_CantidadRecAmbientalUsuario> V_CantidadRecAmbientalUsuario { get; set;}

        public DbSet<Entities.Dashboard.V_CantidadRecPorMesyAnio> V_CantidadRecPorMesyAnio { get; set; }

        public DbSet<Entities.Dashboard.V_TotalReclamosAdmin> V_TotalReclamosAdmin { get; set; }
        public DbSet<Entities.Dashboard.V_TotalTipoReclamosAdmin> V_TotalTipoReclamosAdmin { get; set; }
        public DbSet<Entities.Dashboard.V_TotalReclamosPorAnioAdmin> V_TotalReclamosPorAnioAdmin { get; set; }

        public DbSet<Entities.Dashboard.V_TotalRecAmbientalAdmin> V_TotalRecAmbientalAdmin { get; set; }

        // Menu
        public DbSet<Entities.Menu.V_rolUsuario> V_rolUsuario { get; set; }

        // Login
        public DbSet<Entities.Login.V_ultimaSesionDelUsuario> V_ultimaSesionDelUsuario { get; set; }

    }
}
