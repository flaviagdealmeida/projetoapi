create table Usuario(
	IdUsuario	integer	identity(1,1),
	Nome	nvarchar(100) not null,
	Login	nvarchar(25) not null unique,
	Senha	nvarchar(50) not null,
	primary key(IdUsuario))
