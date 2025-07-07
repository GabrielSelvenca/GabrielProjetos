using System;
using System.Collections.Generic;

namespace WS_Tower.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public int Funcaoid { get; set; }

    public virtual Funcao Funcao { get; set; } = null!;

    public virtual ICollection<Relato> Relatos { get; set; } = new List<Relato>();
}
