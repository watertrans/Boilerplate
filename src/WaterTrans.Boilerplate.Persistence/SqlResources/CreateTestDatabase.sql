CREATE DATABASE IF NOT EXISTS `BoilerplateTest`;
CREATE USER IF NOT EXISTS 'test'@'%' IDENTIFIED BY 'test';
GRANT ALL ON `BoilerplateTest`.* TO 'test'@'%';
