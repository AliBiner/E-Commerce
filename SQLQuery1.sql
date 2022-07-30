create trigger StokAzalt
on sales
after insert
as
declare @urun int
declare @adet int
select @urun=ProductId , @adet=Quantity from inserted
update Products set Stock = Stock - @adet where Id = @urun