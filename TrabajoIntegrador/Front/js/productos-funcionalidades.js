function mostrarTablaProductos() {
    fetch('https://localhost:7253/Productos/GetListadoProductos') //Esta mal la url, hay que cambiarla para que traiga listado de productos
        .then(response => response.json())
        .then(data => {
            var listadoProductosElement = document.getElementById('listado-productos'); 
            var tbody = document.createElement('tbody');

            data.forEach(producto => {
                if (producto.cantidadStock < producto.stockMinimo){  
                    var fila = document.createElement("tr");
                    fila.innerHTML =`
                        <td>${producto.nombreProducto}</td>
                        <td>${producto.marca}</td>
                        <td>${producto.anchoCaja}</td>
                        <td>${producto.profundidadCaja}</td>
                        <td>${producto.altoCaja}</td>
                        <td>${producto.precioProducto}</td>
                        <td>${producto.cantidadStock}</td>`
                    tbody.appendChild(fila);                    
                } 
            });
            listadoProductosElement.appendChild(tbody);
        })
        .catch(error => console.error('Error:', error));
}
document.addEventListener('DOMContentLoaded', mostrarTablaProductos);