using System;
using System.Collections.Generic;

namespace ConsumoAPIMaui.Models;

public partial class Pedido
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public DateTime DataPedido { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<PedidoIten> PedidoItens { get; set; } = new List<PedidoIten>();
}
