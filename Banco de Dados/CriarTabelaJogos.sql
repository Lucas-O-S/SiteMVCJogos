use AULADB
go

--Tabela de Categorias
create table categorias(
	id int not null primary key,
	nome varchar(100)  not null
)

go

--Tabela de jogos
CREATE TABLE jogos( 
 [id] [int] NOT NULL  primary key, 
 [descricao] [varchar](50) NULL, 
 [valor_locacao] [decimal](18, 2) NULL, 
 [data_aquisicao] [datetime] NULL, 
 [categoriaID] [int] NULL
 foreign key ([categoriaID]) references categorias(id)
 )

 go

 ---Cadastros de categoria
 insert into categorias (id,nome) values (1,'Plataforma')
 go
 insert into categorias (id,nome) values (2,'Ação')
  go
insert into categorias (id,nome) values (3,'Puzzle')
go
 insert into categorias (id,nome) values (4,'FPS')
 go

--SPs

create or alter procedure spIncluiJogo(
	@id int,
	@descricao varchar(50),
	@valor_locacao decimal(18,2),
	@data_aquisicao datetime,
	@categoriaID int
)
as
begin
	insert into jogos(id,descricao,valor_locacao,data_aquisicao,categoriaID)
	values (@id,@descricao,@valor_locacao,@data_aquisicao,@categoriaID)
end

go


create or alter procedure spAlteraJogo(
	@id int,
	@descricao varchar(50),
	@valor_locacao decimal(18,2),
	@data_aquisicao datetime,
	@categoriaID int
)
as
begin
	update jogos set descricao = @descricao, valor_locacao =	@valor_locacao,
	data_aquisicao = @data_aquisicao, categoriaID = @categoriaID where id = @id
end
go


create or alter procedure spExcluiJogo(
	@id int
)
as
begin
	delete jogos where id = @id
end
go


create or alter procedure spConsultaJogo(
	@id int
)
as
begin
	select * from jogos where id = @id
end
go



create or alter procedure spListaJogo
as
begin
	select * from jogos 
end
go


create or alter procedure spProximoId(
	@tabela varchar(max)
)
as
begin
	exec('select isnull(max(id)+1,1) as MAIOR from ' + @tabela) 
end
go

create or alter procedure spListaCidade
as
begin
	select * from categorias
end

go