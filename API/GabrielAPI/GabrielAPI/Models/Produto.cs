using System;
using System.Collections.Generic;

namespace GabrielAPI.Models;

public partial class Produto
{
    public int Id { get; set; }

    public required string Nome { get; set; } = null!;

    public required decimal Preco { get; set; }

    public int Estoque { get; set; }

    public virtual ICollection<PedidoIten> PedidoItens { get; set; } = new List<PedidoIten>();
}
