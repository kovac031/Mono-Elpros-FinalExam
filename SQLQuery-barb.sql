insert into RentedBoardGame values
    (newid(), '2023-04-10', '2023-04-10', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1, (SELECT "Id" FROM "CoreUser" WHERE Username like 'barbaraBejb'),(SELECT "Id" FROM "BoardGame" WHERE Title like 'Dobble') ),
    (newid(), '2023-04-08', '2023-04-08', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1, (SELECT "Id" FROM "CoreUser" WHERE Username like 'kovac031'),(SELECT "Id" FROM "BoardGame" WHERE Title like 'Catan') ),
    (newid(), '2023-04-11', '2023-04-11', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1, (SELECT "Id" FROM "CoreUser" WHERE Username like 'lukaAgo'),(SELECT "Id" FROM "BoardGame" WHERE Title like 'Catan') ),
    (newid(), '2023-04-05', '2023-04-05', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1, (SELECT "Id" FROM "CoreUser" WHERE Username like 'barbaraBejb'),(SELECT "Id" FROM "BoardGame" WHERE Title like 'Dixit') ),
    (newid(), '2023-03-10', '2023-03-20', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 0, (SELECT "Id" FROM "CoreUser" WHERE Username like 'nikolaCes'),(SELECT "Id" FROM "BoardGame" WHERE Title like 'Dobble') ),
    (newid(), '2023-03-19', '2023-03-30', getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 0, (SELECT "Id" FROM "CoreUser" WHERE Username like 'kovac031'),(SELECT "Id" FROM "BoardGame" WHERE Title like 'Ubongo') );