/**
 * Created by guidpt on 9/30/17.
 */


    

jQuery(document).ready(function($) {
    
    var language = {
        "sEmptyTable": "Nenhum registro encontrado",
        "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
        "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
        "sInfoFiltered": "(Filtrados de _MAX_ registros)",
        "sInfoPostFix": "",
        "sInfoThousands": ".",
        "sLengthMenu": "_MENU_ resultados por página",
        "sLoadingRecords": "Carregando...",
        "sProcessing": "Processando...",
        "sZeroRecords": "Nenhum registro encontrado",
        "sSearch": "Pesquisar",
        "oPaginate": {
            "sNext": "Próximo",
            "sPrevious": "Anterior",
            "sFirst": "Primeiro",
            "sLast": "Último"
        },
        "oAria": {
            "sSortAscending": ": Ordenar colunas de forma ascendente",
            "sSortDescending": ": Ordenar colunas de forma descendente"
        }
    };
    
    $("#logout").click(function () {
        show_loading_bar(70); // Fill progress bar to 70% (just a given value)
        $.ajax({
            url: "../Account/logout",
            method: 'POST',
            dataType: 'json',
            success: function(resp) {
                show_loading_bar({
                    delay: .5,
                    pct: 100,
                    finish: function() {

                        // Redirect after successful login page (when progress bar reaches 100%)
                        if (resp.ok) {
                            window.location.href = '/';
                        }
                    }
                });
            }
        });
    });
});