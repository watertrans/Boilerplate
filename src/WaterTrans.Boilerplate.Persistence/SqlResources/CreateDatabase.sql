CREATE DATABASE IF NOT EXISTS `Boilerplate`;
CREATE USER IF NOT EXISTS 'user'@'%' IDENTIFIED BY 'user';
GRANT ALL ON `Boilerplate`.* TO 'user'@'%';
