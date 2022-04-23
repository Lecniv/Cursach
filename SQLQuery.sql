            create database cursovoy;

            use cursovoy

            create table Area (
            id_Area     int primary key identity,
            name_Area   nvarchar(40)  not null,
            PRIMARY KEY CLUSTERED (id_Area ASC)
            );

            go

            --*процедура добавления с возвратом ID
            CREATE PROCEDURE insertArea
                @name_Area nvarchar(40),
                @id_Area int out
            AS
                INSERT INTO Area (name_Area)
                VALUES (@name_Area)
                SET @id_Area=SCOPE_IDENTITY()



            --*виды разъемов
            create table FormConnector(
            id_FormConnector int primary key identity,							-- идентификатор вида разъема
            name_FormConnector nvarchar(40) not null,							--наименование вида разъема
            bool_patch bit,														-- на шнуре
            id_Area int,														-- для тэггирования
            Other_Con nvarchar(40),												-- описание разъема, примечание
            CONSTRAINT FС_to_Area foreign key (id_Area) references Area(id_Area)
            )

go
--процедура добавления с возвратом ID
CREATE PROCEDURE insertCon
    @name_FormConnector nvarchar(40),
    @bool_patch bit,
    @id_Area int,	
    @Other_Con nvarchar(40),
    @id_FormConnector int out
AS
    INSERT INTO FormConnector (name_FormConnector, bool_patch, id_Area, Other_Con)
    VALUES (@name_FormConnector,@bool_patch, @id_Area, @Other_Con)
    SET @id_FormConnector=SCOPE_IDENTITY()






            --*Шаблоны плат
            create table Model(
            id_Form_Pl int primary key identity,								-- идентификатор вида разъема
            id_Area int,														-- для тэггирования
            name_Form_Pl nvarchar(40) not null,									-- наименование вида плат
            sum_Connector int,													-- количество разъемов
            sum_Property nvarchar(40),											-- примечание, описание
            CONSTRAINT Model_to_Area foreign key (id_Area) references Area(id_Area)
            )

            --*Таблица шаблонных разъемов -дополнение к Таблице шаблонов плат
            create table Name_Model(
            id_Form_Pl int ,													-- id вида плат
            Num_Connector int,													-- номер разъема в плате
            name_Connector nvarchar(40),										-- наименование
            id_FormConnector int,
            Primary key (id_Form_Pl,Num_Connector),								-- составной id плата-номер разъема
            CONSTRAINT NM_to_Model foreign key (id_Form_Pl) references Model(id_Form_Pl),
            CONSTRAINT NM_to_MF foreign key (id_FormConnector) references FormConnector(id_FormConnector)
            )

            --*Таблица плат
            create table Platy(
            id_Plat int primary key identity,
            id_Form_Pl int ,													-- id вида плат                         !!!!!!!!!!!!! добавить not null
            id_Area int,														-- для тэггирования
            Number_Pl nvarchar(40),
            Serial_Pl nvarchar(40),
            Invint_Pl nvarchar(40),
            Other_PL nvarchar(40),
            CONSTRAINT Platy_to_Area foreign key (id_Area) references Area(id_Area),
            CONSTRAINT Platy_to_Model foreign key (id_Form_Pl) references Model(id_Form_Pl),
            )

go
--процедура ввода Platy с возвратом id и области
alter PROCEDURE insertPlaty
    @id_Form_Pl int,
    @Number_Pl nvarchar(40),
    @Serial_Pl nvarchar(40),
    @Invint_Pl nvarchar(40),
    @Other_PL nvarchar(40),
    @id_Area int out,
    @id_Plat int out
AS
    SET @id_Area = (select Model.id_Area from Model where Model.id_Form_Pl = @id_Form_Pl)
    INSERT INTO Platy (id_Form_Pl, Number_Pl, Serial_Pl, Invint_Pl, Other_PL, id_Area)
    VALUES (@id_Form_Pl, @Number_Pl, @Serial_Pl, @Invint_Pl, @Other_PL, @id_Area )
    SET @id_Plat=SCOPE_IDENTITY()


go
--процедура редактирования Platy
    create PROCEDURE updatePlaty
    @id_Plat int,
    @Number_Pl nvarchar(40),
    @Serial_Pl nvarchar(40),
    @Invint_Pl nvarchar(40),
    @Other_PL nvarchar(40)
    
AS
    update	Platy set   Number_Pl =@Number_Pl ,
                        Serial_Pl =@Serial_Pl ,
                        Invint_Pl =@Invint_Pl ,
                        Other_PL=@Other_PL
    where id_Plat = @id_Plat

go

--процедура удаления Platy
go
create proc delPlaty @del_Pl int
as
begin
	delete from Connector where Connector.id_Plat =  @del_Pl
	delete from Platy where Platy.id_Plat = @del_Pl
end



            --*Таблица взаимодействия разъемов
            create table InOut(
            id_FormConnector_A int ,													
            id_FormConnector_B int,
            Primary key (id_FormConnector_A,id_FormConnector_B),	
            CONSTRAINT InOut_to_A foreign key (id_FormConnector_A) references FormConnector(id_FormConnector),
            CONSTRAINT InOut_to_B foreign key (id_FormConnector_B) references FormConnector(id_FormConnector),
            )


drop table InOut

            --*таблица разъемов-соединений всех плат
            create table Connector(
            id_Connector int primary key identity,
            id_FormConnector int,												-- вид разъема
            id_Plat int,
            name_Connector nvarchar(40),
            id_AFormConnector int,
            CONSTRAINT Con_to_FC foreign key (id_FormConnector) references FormConnector(id_FormConnector),
            CONSTRAINT Con_to_Platy foreign key (id_Plat) references Platy(id_Plat),
            )

drop table Connector


-- ХП с входными переменными
-- сортировка по входному параметру
go
alter proc sortNM_to_Model @p_id_Form_Pl int
as 
begin 
select Name_Model.* from Name_Model
where Name_Model.id_Form_Pl = @p_id_Form_Pl
end

exec sortNM_to_Model '4'

--процедура ввода Model с возвратом id
go
CREATE PROCEDURE insertModel
    @name_Form_Pl nvarchar(40),
    @id_Area int,
    @sum_Connector int,
    @sum_Property nvarchar(40),
    @id_Form_Pl int out
AS
    INSERT INTO Model (id_Area, name_Form_Pl,sum_Connector, sum_Property)
    VALUES (@id_Area,@name_Form_Pl, @sum_Connector,  @sum_Property)
    SET @id_Form_Pl=SCOPE_IDENTITY()

--удаление портов модели по порту модели (при удалении модели)
go
create proc delNM @del_id_Form_Pl int
as 
begin 
delete from Name_Model where id_Form_Pl=@del_id_Form_Pl
end

go
--функция выдает количество разъемов по id  Model 
--работает только в select !!!!!!!!!!! dbo.sum_ConToId(43)
create function sum_ConToId (@id int)
returns int
as
	begin 
	 declare @res int
		select @res=sum_Connector from Model
		where id_Form_Pl = @id
	 return @res
	end

go

--функция столбец управления для PtoP
go
alter function CommdPtoP (@id_AFormConnector int)
returns nvarchar(10)
as
	begin 
	declare @com nvarchar(10)
		if (@id_AFormConnector is null)
			set @com = 'Insert'
		else set @com = 'Delete'
		return @com
	end


--ХП для удаления связей PtoP
go
alter proc delPtoP @id_Con int
as 
declare	@id int--переменная
begin 
 select @id=Connector.id_AFormConnector from Connector where Connector.id_Connector = @id_Con
 update	Connector set Connector.id_AFormConnector = null where id_Connector =  @id_Con
 update	Connector set @id=Connector.id_AFormConnector = null where id_Connector =  @id

end

go

            --триггер при добавлении записи в платах создаются порты в разъемах
            alter trigger tr_add_PlCon
            on Platy
            after insert 
            as
            declare @id int = 0;
            declare @idfp int =0;
            Select @id=id_Plat,@idfp=id_Form_Pl from inserted;
            begin

             insert into Connector (id_FormConnector,inserted.id_Plat,name_Connector)		--заполняем таблицу разъемов
	            select id_FormConnector,inserted.id_Plat,name_Connector from Name_Model			--берем из шаблона
	            join inserted on inserted.id_Form_Pl =  Name_Model.id_Form_Pl;					--фильтруем по добавляемой плате

            end



--удаление портов и модели при удалении шаблона
go
alter proc delConPl @del_Pl int
as 
begin 
delete from Name_Model where id_Form_Pl = @del_Pl
delete from Model where id_Form_Pl = @del_Pl
end


            go
            --триггер удаления взаимосвязанных разъемов
            alter trigger tr_del_refCon
            on Connector
            after delete 
            as

            begin
	            update	Connector set Connector.id_AFormConnector = null where id_Connector in (select deleted.id_AFormConnector from deleted)
            end



--ХП по добавлению связей в InOut, сразу создается связь A-B и B-A
go
Create proc insertInOut @a int, @b int
as 
begin 
insert into InOut values (@a,@b)
insert into InOut values (@b,@a)
end

--ХП по удалению связей в InOut, сразу удаляется связь A-B и B-A
go
Create proc delInOut @a int, @b int
as 
begin 
delete from InOut where id_FormConnector_A = @a and id_FormConnector_B = @b
delete from InOut where id_FormConnector_A = @b and id_FormConnector_B = @a
end

go
--ХП платы присоединенные к разъёмам 
alter proc PlatyToPlaty @id_Plat int
as 
begin 
select Connector.id_Connector,Connector.name_Connector , Model.name_Form_Pl +':'+t.name_Connector as Mod_Con, Platy.Number_Pl, Platy.Serial_Pl, Platy.Invint_Pl,Platy.Other_PL, dbo.CommdPtoP(Connector.id_AFormConnector) as Command from Connector	--+':'+t.name_Connector 
	left join (select * from Connector) t on Connector.id_AFormConnector = t.id_Connector
	left join Platy on Platy.id_Plat = t.id_Plat 
	left join Model on Model.id_Form_Pl = Platy.id_Form_Pl
	where Connector.id_Plat = @id_Plat
end



go
--ХП платы которые можно присоединить по разъемам 
alter proc PlatyFindPlaty @id_Con int
as 
begin 
select t.id_Connector as findID,Connector.name_Connector, Model.name_Form_Pl+':'+t.name_Connector as FindPlaty, Platy.Number_Pl, Platy.Serial_Pl, Platy.Invint_Pl,Platy.Other_PL, 'Insert' as Command from Connector
join InOut on InOut.id_FormConnector_A = Connector.id_FormConnector and  Connector.id_Connector=@id_Con
join (select * from Connector) t on InOut.id_FormConnector_B = t.id_FormConnector  and t.id_AFormConnector is null  --свободные 
join Platy on Platy.id_Plat = t.id_Plat
join Model on Model.id_Form_Pl = Platy.id_Form_Pl

end




--ХП для добавления связей PtoP между платами (разъемами)
go
Create proc AddConnectoin 
@idA int,
@idB int
as 
begin 
 update	Connector set Connector.id_AFormConnector = @idA where id_Connector = @idB
 update	Connector set Connector.id_AFormConnector = @idB where id_Connector = @idA
 
end

--поиск по имени
go
Create proc FindPlatyNum @findText nvarchar(40)
as 
begin 
 select Platy.id_Plat,Model.name_Form_Pl,Area.name_Area,Platy.Number_Pl,Platy.Serial_Pl,Platy.Invint_Pl,Platy.Other_PL from Platy
 join Model on Platy.id_Form_Pl = Model.id_Form_Pl
 join Area on Platy.id_Area = Area.id_Area
 where Platy.Number_Pl LIKE ('%'+@findText+'%')
end


go
Create proc FindPlatySerial @findText nvarchar(40)
as 
begin 
 select Platy.id_Plat,Model.name_Form_Pl,Area.name_Area,Platy.Number_Pl,Platy.Serial_Pl,Platy.Invint_Pl,Platy.Other_PL from Platy
 join Model on Platy.id_Form_Pl = Model.id_Form_Pl
 join Area on Platy.id_Area = Area.id_Area
 where Platy.Serial_Pl LIKE ('%'+@findText+'%')
end

go
Create proc FindPlatyInv @findText nvarchar(40)
as 
begin 
 select Platy.id_Plat,Model.name_Form_Pl,Area.name_Area,Platy.Number_Pl,Platy.Serial_Pl,Platy.Invint_Pl,Platy.Other_PL from Platy
 join Model on Platy.id_Form_Pl = Model.id_Form_Pl
 join Area on Platy.id_Area = Area.id_Area
 where Platy.Invint_Pl LIKE ('%'+@findText+'%')
end

go
Create proc FindPlatyOther @findText nvarchar(40)
as 
begin 
 select Platy.id_Plat,Model.name_Form_Pl,Area.name_Area,Platy.Number_Pl,Platy.Serial_Pl,Platy.Invint_Pl,Platy.Other_PL from Platy
 join Model on Platy.id_Form_Pl = Model.id_Form_Pl
 join Area on Platy.id_Area = Area.id_Area
 where Platy.Other_PL LIKE ('%'+@findText+'%')
end