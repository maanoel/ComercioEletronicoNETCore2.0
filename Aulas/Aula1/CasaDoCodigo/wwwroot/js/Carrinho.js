class Carrinho {
    clickIncremento(button) {
        let data = this.obterData(button);
        data.Quantidade++;
        this.postarQuantidade(data);
    }

    clickDecremento(button) {
        let data = this.obterData(button);
        data.Quantidade--;
        this.postarQuantidade(data);
    }

    postarQuantidade(data) {

        let token = $('[name=__RequestVerificationToken]').val();
        let headers = {};
        headers['RequestVerificationToken'] = token;

        $.ajax({
            url: '/pedido/updatequantidade',
            type: 'post',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: headers
        }).done(function (response) {
            let itemPedido = response.itemPedido;
            let linhaDoItem = $('[item-id=' + itemPedido.id + ']');

            linhaDoItem.find('input').val(itemPedido.quantidade);
            linhaDoItem.find('[subtotal]').html((itemPedido.subtotal).duasCasas());

            let carrinhoViewModel = response.carrinhoViewModel;
            $('[numero-itens]').html('Total: ' + carrinhoViewModel.itens.length + 'itens');
            $('[total]').html((carrinhoViewModel.total).duasCasas());
            if (itemPedido.quantidade == 0) {
                linhaDoItem.remove();
            }
        });
    }

    atualizarQuantidade(input) {
        let data = this.obterData(input);
        this.postarQuantidade(data);
    }

    obterData(elemento) {
        let linhaDoItem = $(elemento).parents('[item-id]');
        let itemId = $(linhaDoItem).attr('item-id');
        let novaQuantidade = $(linhaDoItem).find('input').val();

        return  {
            Id: itemId,
            Quantidade: novaQuantidade
        };
    }
}

var carrinho = new Carrinho();

Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
}
