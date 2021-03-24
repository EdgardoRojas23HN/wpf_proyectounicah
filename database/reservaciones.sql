USE tempdb
go

--Crear la base de datos
Create database Reservaciones
go

--Utilizar la base de datos
use reservaciones
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

create table Usuarios.Usuario(
	id int not null identity (500, 1),
	nombreCompleto varchar(255) not null,
	username varchar(100) not null, 
	password varchar(100) not null,
	estado BIT not null,
	constraint PK_Usuario_id
	primary key clustered(id)
	)
go

--restricciones de la tabla

--el estado de las habitaciones es: ocupada, disponible, mantenimiento  y fuera de servicio
ALTER TABLE Habitaciones.Habitacion With check
add constraint CHK_Habitaciones_Habitacion$EstadoHabitaciones
check (estado IN('OCUPADA', 'DISPONIBLE', 'MANTENIMIENTO', 'FUERADESERVICIO'))

--LLAVE FORANEA PARA LAS HABOITACIONES
ALter table Habitaciones.Reservacion
	add constraint FK_Habitaciones_Reservacion$TieneUna$Habitaciones_Habitaciones
	foreign key (habitacion) references Habitaciones.Habitacion(id)
	on update no action
	on delete no action
go

--La fecha de ingreso no puede ser menor que la fecha actual
Alter table Habitaciones.Reservacion with check
	add constraint CHK_Habitaciones_Habitacion$VerificarFechaIngreso
	check (fechaIngreso >= GETDATE())
GO

--La fecha de salida no puede ser menor o igual ala fecha de ingreso
alter table Habitaciones.Reservacion with check
	add constraint CHK_Habitaciones_Habitacion$VerificarFechaSalida
	check (fechaSalida > fechaIngreso)
go

--No puede existir nombres de usuarios repetidos
Alter Table Usuarios.Usuario 
	add constraint AK_Usuarios_Usuario_username
	unique NONCLUSTERED (username)
	
go

--la contraseña debe contener al menos 6 caracteres
	ALTER TABLE Usuarios.Usuario with check
	add constraint CHK_Usuarios_Usuario$VerificarLongitudContraseña
	check (LEN(password) >=6 )
go


--insert into Habitaciones.Habitacion (id, descripcion, numero, estado) VALUES (1, 'Habitacion sencilla', 10, 'NO DISPONIBLE') 
