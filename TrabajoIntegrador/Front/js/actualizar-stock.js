
    document.getElementById('actualizar-stock-form').addEventListener('submit', function(event){
        event.preventDefault();
        const id = document.getElementById('id').value;
        const cantidadStock = document.getElementById('cantidadStock').value;
        
        fetch(`https://localhost:7253/Productos/ActualizarProducto/${id}`, {
            method: 'PUT',
            headers:{
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(cantidadStock)
        })  
        .then(response => response.json())
        .then(data => {
            console.log('Respuesta de la API', data)
            alert(data.join('\n'))
        })
    })
