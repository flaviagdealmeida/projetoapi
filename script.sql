create table Produto(
	IdProduto		integer		identity(1,1),
	Nome			nvarchar(150)	not null,
	Preco			decimal(18,2)	not null,
	Quantidade		integer		not null,
	primary key(IdProduto))
