using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIPruebas.Models;

public partial class Series
{
    public int IdSerie { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Genero { get; set; }

    public int? AnioEstreno { get; set; }

    public int? NumeroTemporadas { get; set; }

    public string? Estado { get; set; }

    public string? Descripcion { get; set; }

    [JsonIgnore]
    public virtual ICollection<Usuario> oUsuarios { get; set; } = new List<Usuario>();
}
