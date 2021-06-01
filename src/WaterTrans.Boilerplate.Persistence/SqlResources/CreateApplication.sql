﻿CREATE TABLE IF NOT EXISTS `Application` (
    `ApplicationId` CHAR(36) NOT NULL
  , `ClientId` VARCHAR(100) NOT NULL
  , `ClientSecret` VARCHAR(100) NOT NULL
  , `Name` VARCHAR(100) NOT NULL
  , `Description` VARCHAR(400) NOT NULL
  , `Roles` TEXT NOT NULL
  , `Scopes` TEXT NOT NULL
  , `GrantTypes` TEXT NOT NULL
  , `RedirectUris` TEXT NOT NULL
  , `PostLogoutRedirectUris` TEXT NOT NULL
  , `Status` VARCHAR(20) NOT NULL
  , `CreateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
  , `UpdateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
  , PRIMARY KEY (`ApplicationId`)
);

CREATE UNIQUE INDEX IF NOT EXISTS `ix_Application_ClientId` ON `Application` (`ClientId`);
