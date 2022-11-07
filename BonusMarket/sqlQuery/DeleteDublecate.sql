SELECT MIN(Id) OrigId, Sku,
       COUNT(*) TotalCount
FROM   Products
GROUP  BY Sku
HAVING COUNT(*) > 1

select Id 
from Products
where Sku in (SELECT  Sku
FROM   Products
GROUP  BY Sku
HAVING COUNT(*) > 1) and Id not in (SELECT MIN(Id) OrigId
FROM   Products
GROUP  BY Sku
HAVING COUNT(*) > 1)

Delete Products
where Id in (select Id 
from Products
where Sku in (SELECT  Sku
FROM   Products
GROUP  BY Sku
HAVING COUNT(*) > 1) and Id not in (SELECT MIN(Id) OrigId
FROM   Products
GROUP  BY Sku
HAVING COUNT(*) > 1))

Delete Product_To_Picture
where ProductId in (select Id 
from Products
where Sku in (SELECT  Sku
FROM   Products
GROUP  BY Sku
HAVING COUNT(*) > 1) and Id not in (SELECT MIN(Id) OrigId
FROM   Products
GROUP  BY Sku
HAVING COUNT(*) > 1))

Delete ProductTranslation
where ProductId in (select Id 
from Products
where Sku in (SELECT  Sku
FROM   Products
GROUP  BY Sku
HAVING COUNT(*) > 1) and Id not in (SELECT MIN(Id) OrigId
FROM   Products
GROUP  BY Sku
HAVING COUNT(*) > 1))

Delete Product_To_Category
where ProductId in (select Id 
from Products
where Sku in (SELECT  Sku
FROM   Products
GROUP  BY Sku
HAVING COUNT(*) > 1) and Id not in (SELECT MIN(Id) OrigId
FROM   Products
GROUP  BY Sku
HAVING COUNT(*) > 1))