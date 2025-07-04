using System;
using System.Collections.Generic;

namespace ConsumoAPIMaui.Models;

public partial class PedidoIten
{
    public int Id { get; set; }

    public int PedidoId { get; set; }

    public int ProdutoId { get; set; }

    public int Quantidade { get; set; }

    public decimal PrecoUnitario { get; set; }

    public virtual Pedido Pedido { get; set; } = null!;

    public virtual Produto Produto { get; set; } = null!;
}
