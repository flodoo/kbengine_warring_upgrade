# -*- coding: utf-8 -*-
import KBEngine
import GlobalDefine
from KBEDebug import * 
import d_player_attributes

class CombatPropertys:
	def __init__(self):
		#self.HP_Max = 100
		#self.MP_Max = 100
		#setHPMax(self, hpmax)
		if self.isPlayer():
			self.calculateHM_Max()
			self.calculateAD()
		else:
			self.PhyAtack = 5
			self.PhyDef = 0
			self.HP_Max = 100
			self.MP_Max = 100
		#if len(self.SkillList) == 0:
		#	tempskill = []
		#	tempskill.append(1)
		#	tempskill.append(1000101)
		#	tempskill.append(2000101)
		#	#tempskill.append(3000101)
		#	tempskill.append(4000101)
		#	tempskill.append(5000101)
		#	tempskill.append(6000101)
		#	self.SkillList = tempskill
		# 非死亡状态才需要补满
		if not self.isState(GlobalDefine.ENTITY_STATE_DEAD) and self.HP == 0 and self.MP == 0:
			self.fullPower()
	
	def fullPower(self):
		"""
		"""
		self.setHP(self.HP_Max)
		self.setMP(self.MP_Max)
		
	def calculateHM_Max(self):
		maxhp = d_player_attributes.player_attr[self.level]['HP_base']
		maxmp = d_player_attributes.player_attr[self.level]['MP_base']
		self.setHPMax(maxhp)
		self.setMPMax(maxmp)
		
	def calculateAD(self):
		pa = d_player_attributes.player_attr[self.level]['PysAttack']
		pd = d_player_attributes.player_attr[self.level]['PysDef']
		ma = d_player_attributes.player_attr[self.level]['MagicAttack']
		md = d_player_attributes.player_attr[self.level]['MagicDef']
		self.setPysAttackMax(pa)
		self.setPysDefMax(pd)
		self.setMagicAttackMax(ma)
		self.setMagicDefMax(md)
		
	def addHP(self, val):
		"""
		defined.
		"""
		v = self.HP + int(val)
		if v >= self.HP_Max:
			self.HP = self.HP_Max
			return
			
		if v < 0:
			v = 0
			
		if self.HP == v:
			return
			
		self.HP = v
			
	def addMP(self, val):
		"""
		defined.
		"""
		v = self.MP + int(val)
		if v >= self.MP:
			self.MP = self.MP
			return
		if v < 0:
			v = 0
			
		if self.MP == v:
			return
			
		self.MP = v
	
	def setlevel(self, vaule):
		self.level = vaule
		
	def setHP(self, hp):
		"""
		defined
		"""
		hp = int(hp)
		if hp < 0:
			hp = 0
		
		if self.HP == hp:
			return
		if hp > self.HP_Max:
			hp = self.HP_Max
			
		self.HP = hp

	def setMP(self, mp):
		"""
		defined
		"""
		mp = int(mp)
		if mp < 0:
			mp = 0

		if self.MP == mp:
			return
		if mp > self.MP_Max:
			mp = self.MP_Max
			
		self.MP = mp

	def setHPMax(self, hpmax):
		"""
		defined
		"""
		hpmax = int(hpmax)
		self.HP_Max = hpmax
			
	def setMPMax(self, mpmax):
		"""
		defined
		"""
		mpmax = int(mpmax)
		self.MP_Max = mpmax
		
	def setPysAttackMax(self, pa):
		pa = int(pa)
		self.PhyAtack = pa
		
	def addPysAttack(self, pa):
		pa = int(pa)
		self.PhyAtack += pa
		
	def setPysDefMax(self, pd):
		pd = int(pd)
		self.PhyDef = pd
		
	def setMagicAttackMax(self, ma):
		ma = int(ma)
		self.MagicAtack = ma
		
	def setMagicDefMax(self, md):
		md = int(md)
		self.MagicDef = md
		
CombatPropertys._timermap = {}
