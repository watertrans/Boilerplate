﻿CREATE TABLE IF NOT EXISTS `RefreshToken` (
    `Token` VARCHAR(100) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL
  , `Name` VARCHAR(100) NOT NULL
  , `Description` VARCHAR(400) NOT NULL
  , `ApplicationId` CHAR(36) NOT NULL
  , `PrincipalType` VARCHAR(20) NOT NULL
  , `PrincipalId` CHAR(36) NULL
  , `Scopes` TEXT NOT NULL
  , `Status` VARCHAR(20) NOT NULL
  , `ExpiryTime` DATETIME NOT NULL
  , `CreateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
  , `UpdateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
  , `ConcurrencyToken` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
  , PRIMARY KEY (`Token`)
);