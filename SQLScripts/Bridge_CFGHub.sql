USE [Paste Configuration Hub Database Name Here]
GO

ALTER TABLE [dbo].[CFGHub_ConfigAtom]  WITH CHECK ADD  CONSTRAINT [FK_CFGHub_ConfigAtom_CFGHub-Parent] FOREIGN KEY([ParentID])
REFERENCES [dbo].[CFGHub_ConfigAtom] ([ID])
GO
ALTER TABLE [dbo].[CFGHub_ConfigAtom] CHECK CONSTRAINT [FK_CFGHub_ConfigAtom_CFGHub-Parent]
GO

ALTER TABLE [dbo].[CFGHub_ConfigAtom]  WITH CHECK ADD  CONSTRAINT [FK_CFGHub_ConfigAtom_CFGHub-Component] FOREIGN KEY([ComponentID])
REFERENCES [dbo].[CFGHub_SystemComponent] ([ID])
GO
ALTER TABLE [dbo].[CFGHub_ConfigAtom] CHECK CONSTRAINT [FK_CFGHub_ConfigAtom_CFGHub-Component]
GO