document.getElementById('cargar-compra-form').addEventListener('submit', function(event) {
    event.preventDefault();

    const codigoProductoCompra = document.getElementById('codigoProductoCompra').value;
    const dniCliente = document.getElementById('dni').value;
    const cantidadComprada = document.getElementById('cantidadComprada').value;
    const fechaEntregaSolicitada = document.getElementById('fechaEntregaSolicitada').value;

    const datos = {
        CodigoProductoCompra: codigoProductoCompra,
        DniClienteCompra: dniCliente,
        CantidadComprada: cantidadComprada,
        fechaEntregaSolicitada: fechaEntregaSolicitada
    };

    fetch('https://localhost:7253/Compra/AgregarCompra', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(datos)
    })

    .then(response => response.json())
    .then(data => {
        console.log('Respuesta de la API:', data);
        alert(data.join('\n'));
    })
})