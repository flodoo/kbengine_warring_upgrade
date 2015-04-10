# -*- coding: utf-8 -*-
import KBEngine
import random
from KBEDebug import * 

class NormalItem:
	def __init__(self):
		pass
		
	def use(self, userEntity, item):
		"""
		"""
		pass
		
	def showInfo(self, userEntity, item):
		"""
		virtual method.
		显示物品信息
		"""
		pass
		
	def equipOn(self, userEntity, item):
		"""
		virtual method.
		穿上装备
		"""
		pass
		
	def equipOff(self, userEntity, item):
		"""
		virtual method.
		脱下装备
		"""
		pass
		
class SupplyItem(NormalItem):
	"""
	"""
	def __init__(self):
		"""
		"""
		NormalItem.__init__(self)
		
	def use(self, userEntity, item):
		"""
		"""
		userEntity.addHP(item[20])
		userEntity.addMP(item[21])
		
		
	def showInfo(self, userEntity, item):
		"""
		virtual method.
		"""

class EquipItem(NormalItem):
	"""
	"""
	def __init__(self):
		"""
		"""
		NormalItem.__init__(self)
		
	def use(self, userEntity, item):
		"""
		"""
		self.equipOn(userEntity, item)
		
	def showInfo(self, userEntity, item):
		"""
		virtual method.
		"""
	
	def equipOn(self, userEntity, item):
		userEntity.addPysAttack(item[12])
	
