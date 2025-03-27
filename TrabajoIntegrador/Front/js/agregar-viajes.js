document.getElementById('cargar-viaje-form').addEventListener('submit', function(event){
    event.preventDefault();

    const fechaDesde = document.getElementById('fechaDesde').value;
    const fechaHasta = document.getElementById('fechaHasta').value;

    const datos = {
        FechaDesde: fechaDesde,
        FechaHasta: fechaHasta
    };

    fetch('https://localhost:7253/Viajes/AgregarViaje', {
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