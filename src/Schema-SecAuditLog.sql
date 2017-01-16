/****** Object:  Table [dbo].[SecAuditLog]  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SecAuditLog](
	[ID] [uniqueidentifier] NOT NULL,
	[logtime] [datetime] NOT NULL,
	[server] [nvarchar](50) NOT NULL,
	[remote_address] [nvarchar](50) NULL,
	[remote_port] [nvarchar](50) NULL,
	[request_url] [nvarchar](500) NULL,
	[request_headers] [nvarchar](max) NULL,
	[response_protocol] [nvarchar](50) NULL,
	[response_status] [int] NULL,
	[audit_messages] [nvarchar](max) NULL,
	[audit_action_intercepted] [bit] NULL,
	[audit_action_message] [nvarchar](50) NULL,
	[engine_mode] [nvarchar](50) NULL,
	[data] [nvarchar](max) NULL,
 CONSTRAINT [PK_SecAuditLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[SecAuditLog] ADD  CONSTRAINT [DF_SecAuditLog_ID]  DEFAULT (newid()) FOR [ID]
GO


