using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;

namespace TakerylProject
{
	public class ProjectPlayer : ModPlayer
	{
		public bool canSpin = false;
		
	/*	int blinkCharge = 0, blinkRange = 32, blinkDust, blinkCoolDown = 0;
		int radius, Radius; 
		float degrees = 0, degrees2 = 0, degrees3 = 0;
		bool canBlink = false, blinked;
		bool facingRight, facingLeft;
		const int defaultBlinkRange = 32;
		const int TileSize = 16;
		Vector2 dustPos;
		Vector2 center;
		int spinCharge = 0, spinDuration = 600;
		bool canSpin, spinning = false, charging = false;
		int dmgTicks;
		int soundTicks = 0;
		bool thrown = false;
		int throwCharge = 0, throwSpeed = 2;
		int thrownWeapon = 0;
		public int weaponType = 0;
		Vector2 throwPos;
		float CurrentPoint, Depreciating = 0; 
		const float defaultThrowSpeed = 10;		
		const float Time = 100;
		const float gravity = 0.0612f;
		Texture2D swordTexture;
		public override void PreUpdate()
		{
			int TileX = (int)player.position.X/16;
			int TileY = (int)player.position.Y/16;
			if(!canSpin && !canBlink && !blinked && Main.GetKeyState((int)Microsoft.Xna.Framework.Input.Keys.LeftShift) < 0)
			{
				if((player.controlLeft || player.controlRight))
				{
					blinkCharge++;
					if(blinkCharge == 1 || blinkCharge == 60)
					{
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/ShineSparkLoop"), player.position);
					}
				}
				else
				{
					if(blinkCharge > 0)
						blinkCharge--;
				}
				if(blinkCharge >= 117)
				{
					if(player.direction == 1)
					{
						for(int k = 0; k < blinkRange; k++)
						{
							if(CheckRight(TileX + blinkRange, TileY, player))
								blinkRange -= 1;
						}
						facingRight = true;
						facingLeft = false;
					}
					else if(player.direction == -1)
					{
						for(int k = 0; k < blinkRange; k++)
						{
							if(CheckLeft(TileX - blinkRange, TileY, player))
								blinkRange -= 1;
						}
						facingRight = false;
						facingLeft = true;
					}
					if(facingLeft && !CheckLeft(TileX - blinkRange, TileY, player) 
					|| facingRight && !CheckRight(TileX + blinkRange, TileY, player))
					{
						canBlink = true;
						blinkCharge = 0;
					}
					else 
					{
						blinkRange = defaultBlinkRange;
						blinkCharge = 0;
					}
				}
			}
			if(canBlink)
			{
				player.velocity = Vector2.Zero;
				if(blinkCharge == 0)
				{
					Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/ChargeStartup"), player.position);
				}
				blinkCharge++;
				degrees+=0.06f;
				
				radius = 64;
				double X = (radius*Math.Cos(radius*2/(180/Math.PI)));
				double Y = (radius*Math.Sin(radius*2/(180/Math.PI)));
				center = player.position + new Vector2(player.width/2, player.height/2);
				blinkDust = Dust.NewDust(center, 8, 8, 71, 0f, 0f, 0, Color.White, 1.0f);
				Main.dust[blinkDust].noGravity = true;
				Main.dust[blinkDust].position.X = center.X + (float)(radius*Math.Cos(degrees));
				Main.dust[blinkDust].position.Y = center.Y + (float)(radius*Math.Sin(degrees));
				#region points on circle
				if(blinkCharge >= 15)
				{
					int degree45 = Dust.NewDust(player.position + new Vector2((float)X, (float)Y), 8, 8, 71, 0f, 0f, 0, Color.White, 0.5f);
					Main.dust[degree45].noGravity = false;
					Main.dust[degree45].position.X = center.X + (float)(radius*Math.Cos(45));
					Main.dust[degree45].position.Y = center.Y + (float)(radius*Math.Sin(45));
				}
				if(blinkCharge >= 30)
				{
					int degree90 = Dust.NewDust(player.position + new Vector2((float)X, (float)Y), 8, 8, 71, 0f, 0f, 0, Color.White, 0.5f);
					Main.dust[degree90].noGravity = false;
					Main.dust[degree90].position.X = center.X + (float)(radius*Math.Cos(90));
					Main.dust[degree90].position.Y = center.Y + (float)(radius*Math.Sin(90));
				}
				if(blinkCharge >= 45)
				{
					int degree135 = Dust.NewDust(player.position + new Vector2((float)X, (float)Y), 8, 8, 71, 0f, 0f, 0, Color.White, 0.5f);
					Main.dust[degree135].noGravity = false;
					Main.dust[degree135].position.X = center.X + (float)(radius*Math.Cos(135));
					Main.dust[degree135].position.Y = center.Y + (float)(radius*Math.Sin(135));
				}
				if(blinkCharge >= 60)
				{
					int degree180 = Dust.NewDust(player.position + new Vector2((float)X, (float)Y), 8, 8, 71, 0f, 0f, 0, Color.White, 0.5f);
					Main.dust[degree180].noGravity = true;
					Main.dust[degree180].position.X = center.X + (float)(radius*Math.Cos(180));
					Main.dust[degree180].position.Y = center.Y + (float)(radius*Math.Sin(180));
				}
				if(blinkCharge >= 75)
				{
					int degree225 = Dust.NewDust(player.position + new Vector2((float)X, (float)Y), 8, 8, 71, 0f, 0f, 0, Color.White, 0.5f);
					Main.dust[degree225].noGravity = true;
					Main.dust[degree225].position.X = center.X + (float)(radius*Math.Cos(225));
					Main.dust[degree225].position.Y = center.Y + (float)(radius*Math.Sin(225));
				}
				if(blinkCharge >= 90)
				{
					int degree270 = Dust.NewDust(player.position + new Vector2((float)X, (float)Y), 8, 8, 71, 0f, 0f, 0, Color.White, 0.5f);
					Main.dust[degree270].noGravity = true;
					Main.dust[degree270].position.X = center.X + (float)(radius*Math.Cos(270));
					Main.dust[degree270].position.Y = center.Y + (float)(radius*Math.Sin(270));
				}
				if(blinkCharge >= 117)
				{
					int degree315 = Dust.NewDust(player.position + new Vector2((float)X, (float)Y), 8, 8, 71, 0f, 0f, 0, Color.White, 0.5f);
					Main.dust[degree315].noGravity = true;
					Main.dust[degree315].position.X = center.X + (float)(radius*Math.Cos(315));
					Main.dust[degree315].position.Y = center.Y + (float)(radius*Math.Sin(315));
				}
				#endregion
				if(blinkCharge >= 117)
				{
					Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/teleport"), player.oldPosition);
					if(facingRight)
					{
						player.position.X += TileSize*blinkRange;
						facingRight = false;
					}
					else if(facingLeft)
					{
						player.position.X -= TileSize*blinkRange;
						facingLeft = false;
					}
					for(int k = 0; k < 360/2; k++)
					{
						radius = 16;
						blinkDust = Dust.NewDust(player.Center, 8, 8, 71, 0f, 0f, 0, Color.White, 1.2f);
						Main.dust[blinkDust].noGravity = true;
						Main.dust[blinkDust].velocity.X = (float)(radius*Math.Cos(k));
						Main.dust[blinkDust].velocity.Y = (float)(radius*Math.Sin(k));
					}
					blinkRange = defaultBlinkRange;
					blinkCharge = 0;
					blinkCoolDown = 360;
					degrees = 0;
					canBlink = false;
					blinked = true;
				}
			}
			if(blinkCoolDown > 0) 
				blinkCoolDown--;
			if(blinkCoolDown == 0) 
				blinked = false;
			Item item = player.inventory[player.selectedItem];
			if(!canSpin && !spinning && Main.mouseMiddle && item.type == 368)
			{
				spinCharge++;
				if(spinCharge >= 60)
				{
					canSpin = true;
					spinCharge = 0;
					spinDuration = 600;
				}
				if(spinCharge == 30)
					Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/conjure"), player.position);
			}
			else
			{
				if(spinCharge > 0) 
					spinCharge--;
			}
			if(canSpin)
			{
				blinkCharge = 0;
				if(item.type == 368)
				{
					spinning = true;
					
					radius = 72;
					degrees2 += 0.06f;
					
					center = player.position + new Vector2(player.width/2, player.height/2);
					
					float PosX = center.X + (float)(radius*Math.Cos(degrees2));
					float PosY = center.Y + (float)(radius*Math.Sin(degrees2));	
					
					Vector2 swordPos = player.bodyPosition + new Vector2(PosX, PosY);
					
					Rectangle swordHitBox = new Rectangle((int)swordPos.X, (int)swordPos.Y, item.width, item.height);
					NPC[] npc = Main.npc;
					for(int k = 0; k < npc.Length-1; k++)
					{
						NPC n = npc[k];
						Vector2 npcv = new Vector2(n.position.X, n.position.Y);
						Rectangle npcBox = new Rectangle((int)npcv.X, (int)npcv.Y, n.width, n.height);
						if(n.active && !n.friendly && !n.dontTakeDamage 
						&& dmgTicks == 0 && swordHitBox.Intersects(npcBox))
						{
							n.StrikeNPC((int)(item.damage + player.meleeDamage), item.knockBack*3, player.direction,false,false);
							dmgTicks = 10;
						}
					}
					Projectile[] projectile = Main.projectile;
					for(int l = 0; l < projectile.Length-1; l++)
					{
						Projectile n = projectile[l];
						Vector2 projv = new Vector2(n.position.X, n.position.Y);
						Rectangle projBox = new Rectangle((int)projv.X, (int)projv.Y, n.width, n.height);
						if(n.active && swordHitBox.Intersects(projBox))
						{
							n.timeLeft = 0;
						}
					}
					
					soundTicks++;
					if(soundTicks%16 == 0)
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/SpinQuiet"), player.bodyPosition + new Vector2(PosX, PosY));
				}
				if(spinDuration > 0) 
					spinDuration--;
				if(spinDuration == 0)
				{
					degrees2 = 0f;
					canSpin = false;
					spinning = false;
				}
			}
			if(dmgTicks > 0) dmgTicks--;
			if(!thrown && item.type == 368 && Main.mouseRight)
			{
				throwCharge++;
				if(throwCharge == 90)
				{
					thrown = true;
					throwCharge = 0;
					Depreciating = Time;
				}
			}
			if(thrown)
			{
				throwCharge++;
				if(throwCharge == 600)
				{
					thrown = false;
					throwCharge = 0;
				}
			}
			if(Depreciating > 0) 
				Depreciating--;
		}
		
		public bool CheckLeft(int i, int j, Player player)
		{
			bool Active = Main.tile[i-1, j].active() || Main.tile[i-1, j+1].active() || Main.tile[i-1, j +2].active();
			bool Solid = Main.tileSolid[Main.tile[i-1, j].type] == true || Main.tileSolid[Main.tile[i-1, j+1].type] == true || Main.tileSolid[Main.tile[i-1, j+2].type] == true;
			
			if(Active && Solid) return true;
			else return false;
		}
		public bool CheckRight(int i, int j, Player player)
		{
			bool Active = (Main.tile[i+1, j].active() || Main.tile[i+1, j+1].active() || Main.tile[i+1, j +2].active());
			bool Solid = ((Main.tileSolid[Main.tile[i+1, j].type] == true) || (Main.tileSolid[Main.tile[i+1, j+1].type] == true) || (Main.tileSolid[Main.tile[i+1, j+2].type] == true));
			
			if(Active && Solid) return true;
			else return false;
		}	*/
		float degrees3 = 0;
		Texture2D swordTexture;
		public override void DrawEffects(PlayerDrawInfo drawinfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			PlayerHeadDrawInfo drawInfo = new PlayerHeadDrawInfo();
			drawInfo.spriteBatch = Main.spriteBatch;
			drawInfo.drawPlayer = player;
			
			int radius = 72;
			if(/*thrown || */canSpin)
				degrees3 += 0.06f;
			else degrees3 = 0;
			
		//	CurrentPoint = (Time - Depreciating) / Time;
			
			SpriteEffects effects = SpriteEffects.None;
			Vector2 origin = new Vector2((float)player.legFrame.Width * 0.5f, (float)player.legFrame.Height * 0.5f);
			Vector2 bodyPosition = new Vector2((float)((int)(player.position.X - Main.screenPosition.X - (float)(player.bodyFrame.Width / 2) + (float)(player.width / 2))), (float)((int)(player.position.Y - Main.screenPosition.Y + (float)player.height - (float)player.bodyFrame.Height + 4f)));

			float MoveX = origin.X + (float)(radius*Math.Cos(degrees3));
			float MoveY = origin.Y + (float)(radius*Math.Sin(degrees3));
			
			Item item = player.inventory[player.selectedItem];
			if(item.type == 368)
				swordTexture = Main.itemTexture[368];
			if(canSpin)
			{
				Main.spriteBatch.Draw(swordTexture, 
					bodyPosition + player.bodyPosition + new Vector2(MoveX, MoveY), 
					null, Color.White, (degrees3*3)*(-1) + 0.48f, origin, 1f, effects, 0f);
			}
		}
	}
}