class Carrinho {
    clickIncremento(btn) {
        let data = this.obterData(btn);
        data.Quantidade++;
        this.postarQuantidade(data);
    }

    clickDecremento(btn) {
        let data = this.obterData(btn);
        data.Quantidade--;
        this.postarQuantidade(data);
    }

    postarQuantidade(data) {
        $.ajax({
            url: '/pedido/updatequantidade',
            type: 'post',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (response) {
            location.reload();
        });
    }

    atualizarQuantidade(input) {
        let data = this.obterData(input);
        this.postarQuantidade(data);
    }

    obterData(elemento) {
        let linhaDoItem = $(elemento).parents('[item-id]');
        let itemId = $(linhaDoItem).attr('item-id');
        let novaQtde = $(linhaDoItem).find('input').val();

        return  {
            Id: itemId,
            Quantidade: novaQtde
        };
    }
}

var carrinho = new Carrinho();