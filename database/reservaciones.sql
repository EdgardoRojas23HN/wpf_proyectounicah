USE tempdb
go

--Crear la base de datos
Create database Reservaciones
go

--Utilizar la base de datos
use Reservaciones
go

--Crear los schema de la base de datos
create schema Habitaciones
go

create schema Usuarios
go


--Crear las tablas de Habitaciones, Reservaciones y Usuario
CREATE TABLE Habitaciones.Habitacion(
	id INT NOT NULL IDENTITY(1,1),
	descripcion varchar(255) NOT NULL,
	numero INT NOT NULL,
	estado VARCHAR(20) NOT NULL,
	CONSTRAINT PK_Habitacion_id
		PRIMARY KEY CLUSTERED (id)
)
go

create table Habitaciones.Reservacion(
	id INT NOT NULL IDENTITY(1000, 1),
	nombreCompleto varchar (255) not null,
	habitacion int not null,
	fechaIngreso DateTime not null,
	fechaSalida DateTime not null,
	estado BIT not null,
	Constraint PK_Reservacion_id
		Primary key clustered (id)
	)
go