--Category
INSERT INTO Category VALUES
(newid(), 'Party', 'A party board game is a type of board game that is designed for a large group of people to play together in a social setting, 
such as a party or gathering. These games usually involve a lot of interaction and communication between players and often have a light-hearted and humorous tone. 
The gameplay mechanics are typically simple and easy to learn, and the focus is on having fun and enjoying the company of others.',
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username like 'ADMINISTRATOR'), 1);

INSERT INTO Category VALUES
(newid(), 'Word', 'A word board game is a type of board game that involves players forming words or solving word-related puzzles. 
These games usually require a good vocabulary and a strong knowledge of spelling and grammar. The gameplay mechanics can vary widely, 
from crossword-style puzzles to games that involve creating words from a set of letters. Word board games can be played individually or in teams, 
and often involve a mix of strategy and luck.',
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), 1);

INSERT INTO Category VALUES
(newid(), 'Cooperative', 'A cooperative board game is a type of board game where players work together as a team to achieve a common goal, 
rather than competing against each other. The gameplay mechanics typically involve players taking turns to make moves or decisions that will 
benefit the group as a whole, rather than just themselves. These games require communication, collaboration, and strategic planning in order to succeed.', 
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), 1);

--BoardGame
INSERT INTO BoardGame VALUES
(newid(), 'Risk', 6.49, 'Risk is a popular strategy board game that is played on a world map divided into territories. 
The objective of the game is to conquer the world by occupying all territories on the board or eliminating all other players.', 4, 
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), 1, 
(SELECT "Id" FROM "Category" WHERE "Name" = 'Strategy'));

INSERT INTO BoardGame VALUES
(newid(), 'Uno', 4.99, 'The objective of the game is to be the first player to get rid of all their cards by playing cards with matching colors or numbers.', 6, 
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), 1, 
(SELECT "Id" FROM "Category" WHERE "Name" = 'Card'));

INSERT INTO BoardGame VALUES
(newid(), 'Pandemic', 12.49, 'Pandemic is a cooperative board game where players work together as a team to stop the spread of deadly diseases across the world. 
The objective of the game is to discover cures for four different diseases before they become a global pandemic and wipe out humanity.', 2, 
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), 1, 
(SELECT "Id" FROM "Category" WHERE "Name" = 'Cooperative'));

INSERT INTO BoardGame VALUES
(newid(), 'Scrabble', 3.59, 'Players use letter tiles to create words on a game board. The objective of the game is to score as many points as possible 
by forming words with high-scoring letters and placing them on the game board in a crossword-style pattern.', 5, 
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), 1, 
(SELECT "Id" FROM "Category" WHERE "Name" = 'Word'));

INSERT INTO BoardGame VALUES
(newid(), 'Codenames', 14.49, 'The objective of the game is to correctly identify all of your team''s secret agents before the other team does.', 5, 
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), 1, 
(SELECT "Id" FROM "Category" WHERE "Name" = 'Party'));

--Review
INSERT INTO Review VALUES
(newid(), 'Never gets old!', 'Scrabble is a classic game that never gets old. It''s a great way to challenge your vocabulary and strategy skills 
while having fun with friends and family. The game board and tiles are well-made, and the game is easy to learn but hard to master. I highly recommend it!', 1, 5, 
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'nikolaCes'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'nikolaCes'), 1, 
(SELECT "Id" FROM "CoreUser" WHERE Username = 'nikolaCes'), (SELECT "Id" FROM "BoardGame" WHERE "Title" = 'Scrabble'));

INSERT INTO Review VALUES
(newid(), 'Booooring.', 'I was really disappointed with Scrabble. The game is slow and boring, and it feels like you''re just rearranging letters on a board. 
It''s not very engaging or exciting, and the scoring system can be frustrating. I also found the game to be too difficult for casual players, 
and it''s not very fun to play if you''re not good at spelling or vocabulary. I wouldn''t recommend this game unless you''re a die-hard Scrabble fan.', 0, 1, 
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'barbaraBejb'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'barbaraBejb'), 0, 
(SELECT "Id" FROM "CoreUser" WHERE Username = 'barbaraBejb'), (SELECT "Id" FROM "BoardGame" WHERE "Title" = 'Scrabble'));

INSERT INTO Review VALUES
(newid(), 'I have mixed feelings about this game...', 'On one hand, I love the challenge of coming up with new words and trying to score big points. 
The game is well-made, with high-quality tiles and a sturdy game board. However, the game can be slow-paced and can take a while to play, 
which can be frustrating if you''re short on time. Additionally, the scoring system can be confusing at times, and it can be difficult 
to keep track of all the letters on the board. Overall, I would recommend Scrabble to those who enjoy word games and are looking for a mental challenge, 
but it''s not for everyone.', 1, 3, 
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username like 'kovac031'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'kovac031'), 1, 
(SELECT "Id" FROM "CoreUser" WHERE Username = 'kovac031'), (SELECT "Id" FROM "BoardGame" WHERE "Title" = 'Scrabble'));

INSERT INTO Review VALUES
(newid(), 'Fun and challenging!', 'I really enjoyed playing Settlers of Catan. The game is easy to learn but offers a lot of strategic depth, which keeps things interesting. 
The game board and pieces are high-quality and visually appealing, and the game mechanics are well-designed. The game is also highly replayable, 
with different strategies and outcomes each time you play. The only downside is that the game can take a while to play, which may not be suitable for everyone. 
Overall, I highly recommend Settlers of Catan to anyone who enjoys strategy games and is looking for a fun and challenging experience.', 1, 4, 
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'lukaAgo'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'lukaAgo'), 1, 
(SELECT "Id" FROM "CoreUser" WHERE Username = 'lukaAgo'), (SELECT "Id" FROM "BoardGame" WHERE "Title" = 'Catan'));

INSERT INTO Review VALUES
(newid(), 'Uno!', 'I absolutely love Uno! It''s a classic card game that never gets old. The game is easy to learn but has endless possibilities for strategy and 
fun. The different cards add a level of excitement to the game, and the game is perfect for both casual and competitive play. 
The packaging and cards are also well-made and durable, which ensures the game can be enjoyed for years to come. 
I highly recommend Uno to anyone looking for a fun and engaging card game that can be enjoyed by people of all ages.', 1, 5, 
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'lukaAgo'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'lukaAgo'), 1, 
(SELECT "Id" FROM "CoreUser" WHERE Username = 'lukaAgo'), (SELECT "Id" FROM "BoardGame" WHERE "Title" = 'Uno'));

--Notification
INSERT INTO Notification VALUES
(newid(), 'New 24/7 rental option', 'Get ready for game night with PlayPal''s new 24-hour rental option for popular board games like Catan and Scrabble.',
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), 1);

INSERT INTO Notification VALUES
(newid(), 'Become a Premium User!', 'PlayPal introduces a new subscription service for board game enthusiasts, 
offering multiple rentals of classic and modern games at a discount.',
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), 1);

INSERT INTO Notification VALUES
(newid(), 'New Inventory', 'PlayPal announces new inventory of popular board games, including Scrabble and Pandemic, available for rent now.',
getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), 0);

--UserNotification
INSERT INTO UserNotification VALUES
(newid(), getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'),
1, 0, (SELECT "Id" FROM "CoreUser" WHERE Username = 'lukaAgo'), (SELECT "Id" FROM "Notification" WHERE Title = 'New Inventory'));

INSERT INTO UserNotification VALUES
(newid(), getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'),
1, 1, (SELECT "Id" FROM "CoreUser" WHERE Username = 'barbaraBejb'), (SELECT "Id" FROM "Notification" WHERE Title = 'New Inventory'));

INSERT INTO UserNotification VALUES
(newid(), getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'),
0, 0, (SELECT "Id" FROM "CoreUser" WHERE Username = 'lukaAgo'), (SELECT "Id" FROM "Notification" WHERE Title = 'Become a Premium User!'));

INSERT INTO UserNotification VALUES
(newid(), getdate(), getdate(), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'ADMINISTRATOR'),
1, 0, (SELECT "Id" FROM "CoreUser" WHERE Username = 'lukaAgo'), (SELECT "Id" FROM "Notification" WHERE Title = 'Become a Premium User!'));

--UsersDetail
INSERT INTO UsersDetail VALUES
(newid(), 'Nikola', 'Antovski', 2002-12-19, 'Mono 3', getdate(), getdate(), 
(SELECT "Id" FROM "CoreUser" WHERE Username = 'nikolaCes'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'nikolaCes'), 0);

INSERT INTO UsersDetail VALUES
(newid(), 'Luka', 'Agic', 1990-04-26, 'Mono 1', getdate(), getdate(), 
(SELECT "Id" FROM "CoreUser" WHERE Username = 'lukaAgo'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'lukaAgo'), 1);

INSERT INTO UsersDetail VALUES
(newid(), 'Ivan', 'Kovac', 1995-09-06, 'Mono 1', getdate(), getdate(), 
(SELECT "Id" FROM "CoreUser" WHERE Username = 'kovac031'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'kovac031'), 1);

INSERT INTO UsersDetail VALUES
(newid(), 'Barbara', 'Friscic', 1993-06-09, 'Mono 3', getdate(), getdate(), 
(SELECT "Id" FROM "CoreUser" WHERE Username = 'barbaraBejb'), (SELECT "Id" FROM "CoreUser" WHERE Username = 'barbaraBejb'), 1);