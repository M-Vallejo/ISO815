CREATE DATABASE IF NOT EXISTS Termopac;
use Termopac;

CREATE TABLE IF NOT EXISTS Producto (
    producto_id INT AUTO_INCREMENT PRIMARY KEY,
    descripcion VARCHAR(255) NOT NULL,
    cantidad INT,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
)  ENGINE=INNODB;

CREATE TABLE IF NOT EXISTS Venta (
    venta_id INT AUTO_INCREMENT PRIMARY KEY,
    producto_id int Not null REFERENCES Producto (producto_id),
    vendedor VARCHAR(50),
    comprador VARCHAR(50),
    descripcion VARCHAR(255),
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP       
)  ENGINE=INNODB;

-- INSERT INTO producto (descripcion, cantidad) VALUES('Manzana', 10 );

DELIMITER //
 CREATE TRIGGER UpdatequantityProducts 
    after insert ON Venta
    FOR EACH ROW
		begin
			update Producto set Producto.cantidad = Producto.cantidad - 1  where Producto.producto_id = New.producto_id;
		end
    //

-- drop trigger UpdatequantityProducts;
-- INSERT INTO Venta (vendedor, comprador, descripcion, producto_id) VALUES("Risbelly", "Javier", "", 1);

truncate table Producto
-- select * from Termopac.Producto; 
-- select * from Termopac.Venta;

