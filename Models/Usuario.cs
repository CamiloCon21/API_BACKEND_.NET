using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIPruebas.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string? NombreCompleto { get; set; }

    public string? CorreoElectronico { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    public string? Activo { get; set; }

    [JsonIgnore]

    public virtual ICollection<Series> oSeries { get; set; } = new List<Series>();
}
