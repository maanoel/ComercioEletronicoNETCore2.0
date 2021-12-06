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
        })
    }

    obterData(elemento) {
        let linhaDoItem = $(elemento).parents('[item-id]');
        let itemId = $(linhaDoItem).attr('item-id');
        let novaQtde = $(linhaDoItem).find('input').val();

        return data = {
            Id: itemId,
            Quantidade: novaQtde
        };
    }
}

var carrinho = new Carrinho();

