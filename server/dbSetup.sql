CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) UNIQUE COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';


CREATE TABLE albums(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  creator_id VARCHAR(255) NOT NULL,
  title TINYTEXT NOT NULL,
  description TINYTEXT,
  cover_img TEXT NOT NULL,
  archived BOOLEAN NOT NULL DEFAULT false,
  category ENUM('aesthetics', 'food', 'games', 'animals', 'vibes', 'misc') NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
);

DROP TABLE albums;

SELECT
    albums.*,
    accounts.*
    FROM albums
    JOIN accounts ON albums.creator_id = accounts.id
    WHERE albums.id = 2;


CREATE TABLE pictures(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  creator_id VARCHAR(255) NOT NULL,
  album_id INT NOT NULL,
  img_url TEXT NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (album_id) REFERENCES albums(id) ON DELETE CASCADE
);


CREATE TABLE watchers(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  account_id VARCHAR(255) NOT NULL,
  album_id INT NOT NULL,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  FOREIGN KEY (account_id) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (album_id) REFERENCES albums(id) ON DELETE CASCADE
);


SELECT
watchers.*,
accounts.*
FROM watchers
JOIN accounts ON watchers.account_id = accounts.id
WHERE album_id = 19;


SELECT
watchers.*,
albums.*,
accounts.*
FROM watchers
JOIN albums ON watchers.album_id = albums.id
JOIN accounts ON accounts.id = albums.creator_id
WHERE  watchers.account_id = '670ff93326693293c631476f';

INSERT INTO watchers(album_id,account_id )
VALUES(1,'670ff93326693293c631476f' );

SELECT * FROM albums;