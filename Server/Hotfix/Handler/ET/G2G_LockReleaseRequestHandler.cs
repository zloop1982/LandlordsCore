﻿using System;
using Model;

namespace Hotfix
{
	[MessageHandler(AppType.Gate)]
	public class G2G_LockReleaseRequestHandler : AMRpcHandler<G2G_LockReleaseRequest, G2G_LockReleaseResponse>
	{
		protected override void Run(Session session, G2G_LockReleaseRequest message, Action<G2G_LockReleaseResponse> reply)
		{
			G2G_LockReleaseResponse g2GLockReleaseResponse = new G2G_LockReleaseResponse();

			try
			{
				Unit unit = Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
				if (unit == null)
				{
					g2GLockReleaseResponse.Error = ErrorCode.ERR_NotFoundUnit;
					reply(g2GLockReleaseResponse);
					return;
				}

				unit.GetComponent<MasterComponent>().Release(message.Address);
				reply(g2GLockReleaseResponse);
			}
			catch (Exception e)
			{
				ReplyError(g2GLockReleaseResponse, e, reply);
			}
		}
	}
}