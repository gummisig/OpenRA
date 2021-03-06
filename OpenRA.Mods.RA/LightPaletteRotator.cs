﻿#region Copyright & License Information
/*
 * Copyright 2007-2011 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation. For more information,
 * see COPYING.
 */
#endregion

using System.Collections.Generic;
using System.Linq;
using OpenRA.Graphics;
using OpenRA.Traits;

namespace OpenRA.Mods.RA
{
	[Desc("Palette effect used for blinking \"animations\" on actors.")]
	class LightPaletteRotatorInfo : ITraitInfo
	{
		public readonly string[] ExcludePalettes = { };

		public object Create(ActorInitializer init) { return new LightPaletteRotator(this); }
	}

	class LightPaletteRotator : ITickRender, IPaletteModifier
	{
		float t = 0;
		public void TickRender(WorldRenderer wr, Actor self)
		{
			if (wr.world.Paused == World.PauseState.Paused)
				return;

			t += .5f;
		}

		readonly LightPaletteRotatorInfo info;

		public LightPaletteRotator(LightPaletteRotatorInfo info)
		{
			this.info = info;
		}

		public void AdjustPalette(IReadOnlyDictionary<string, MutablePalette> palettes)
		{
			foreach (var pal in palettes)
			{
				if (info.ExcludePalettes.Contains(pal.Key))
					continue;

				var rotate = (int)t % 18;
				if (rotate > 9)
					rotate = 18 - rotate;

				pal.Value.SetColor(0x67, pal.Value.GetColor(230 + rotate));
			}
		}
	}
}
