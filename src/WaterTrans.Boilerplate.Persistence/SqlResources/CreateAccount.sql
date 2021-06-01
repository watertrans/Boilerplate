﻿CREATE TABLE IF NOT EXISTS `Account` (
    `AccountId` CHAR(36) NOT NULL
  , `UserId` CHAR(36) NOT NULL
  , `Roles` TEXT NOT NULL
  , `Status` VARCHAR(20) NOT NULL
  , `LastLoginTime` DATETIME NULL
  , `CreateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
  , `UpdateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
  , PRIMARY KEY (`AccountId`)
);

CREATE UNIQUE INDEX IF NOT EXISTS `ix_Account_UserId` ON `Account` (`UserId`);
