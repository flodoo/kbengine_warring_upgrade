# -*- coding: utf-8 -*-
import KBEngine
import GlobalDefine
from KBEDebug import * 
from interfaces.CombatPropertys import CombatPropertys
import d_entities
import d_knapsackItems
import d_exps
import d_entities_drops
from BAGITEM import BAGITEM_INFOS
import random

class Combat(CombatPropertys):
	def __init__(self):
		CombatPropertys.__init__(self)
		
	def canUpgrade(self):
		"""
		virtual method.
		"""
		return True
		
	def upgrade(self):
		"""
		for real
		"""
		if self.canUpgrade():
			self.addLevel(1)
			
	def addLevel(self, lv):
		"""
		for real
		"""
		self.level += lv
		self.onLevelChanged(lv)
		
	def onLevelChanged(self, addlv):
		"""
		virtual method.
		"""
		pass
		
	def isDead(self):
		"""
		"""
		return self.state == GlobalDefine.ENTITY_STATE_DEAD
		
	def die(self, killerID):
		"""
		"""
		if self.isDestroyed or self.isDead():
			return
		
		if killerID == self.id:
			killerID = 0
			
		INFO_MSG("%s::die: %i i die. killerID:%i." % (self.getScriptName(), self.id, killerID))
		killer = KBEngine.entities.get(killerID)
		if killer:
			killer.onKiller(self.id)
			
		self.onBeforeDie(killerID)
		self.onDie(killerID)
		self.changeState(GlobalDefine.ENTITY_STATE_DEAD)
		self.onAfterDie(killerID)
		
	def onDie(self, killerID):
		"""
		virtual method.
		"""
		self.setHP(0)
		self.setMP(0)

	def onBeforeDie(self, killerID):
		"""
		virtual method.
		"""
		pass

	def onAfterDie(self, killerID):
		"""
		virtual method.
		"""
		datas = d_entities_drops.datas.get(self.uid)
		allitems = d_knapsackItems.allItems
		if datas is None:
			ERROR_MSG("SpawnPoint::spawn:%i not found." % self.uid)
			return
		for k in datas.keys():
			rate = random.randint(0, 100)
			item = BAGITEM_INFOS()
			if datas[k]>rate:
				temp = allitems[k]
				item.extend([0, k, 0, 0, 1, temp["name"], temp["altnasIndex"], temp["icon"], temp["modelId"], temp["stdMode"], temp["weight"], temp["func"], temp["PA1"], temp["PA2"], temp["MA1"], temp["MA2"], temp["PD1"], temp["PD2"], temp["MD1"], temp["MD2"], temp["HP"], temp["MP"], temp["Accurate"], temp["lucky"], temp["holiness"], temp["HPgen"], temp["MPgen"], temp["crit"], temp["need"], temp["needLevel"], temp["price"], temp["stock"], temp["script"]])
				params = {
					"bagItemData"	: item,
					"modelID" : item[8],
					"name" : item[5],
				}
				e = KBEngine.createEntity("DroppedItem", self.spaceID, tuple(self.position), tuple(self.direction), params)
			
	
	def onKiller(self, entityID):
		"""
		defined.
		我击杀了entity
		"""
		if self.isPlayer():
			target = KBEngine.entities.get(entityID)
			getExp = target.getDatas()["GenExp"]
			nowExp = self.Exp + getExp
			needExp = d_exps.exps[self.level]
			if nowExp > needExp:
				self.Exp = nowExp - needExp
				self.setlevel(self.level + 1)
			else:
				self.Exp = nowExp
		pass
	
	def canDie(self, attackerID, skillID, damageType, damage):
		"""
		virtual method.
		是否可死亡
		"""
		return True

	def onDestroy(self):
		"""
		entity销毁
		"""
		pass
		
	def recvDamage(self, attackerID, skillID, damageType, damage):
		"""
		defined.
		"""
		if self.isDestroyed or self.isDead():
			return
		
		DEBUG_MSG("%s::recvDamage: %i attackerID=%i, skillID=%i, damageType=%i, damage=%i" % \
			(self.getScriptName(), self.id, attackerID, skillID, damageType, damage))
		
		damage = damage - self.PhyDef
		if damage < 1:
			damage = 1
		if self.HP <= damage:
			if self.canDie(attackerID, skillID, damageType, damage):
				self.die(attackerID)
		else:
			self.setHP(self.HP - damage)
		
		self.allClients.recvDamage(attackerID, skillID, damageType, damage)
		
	def addEnemy(self, entityID, enmity):
		"""
		defined.
		添加敌人
		"""
		DEBUG_MSG("%s::addEnemy: %i entity=%i, enmity=%i" % \
						(self.getScriptName(), self.id, entityID, enmity))
		
		self.enemyLog.append(entityID)
		self.onAddEnemy(entityID)
	
	def onAddEnemy(self, entityID):
		"""
		virtual method.
		有敌人进入列表
		"""
		pass
	
	def removeEnemy(self, entityID):
		"""
		defined.
		删除敌人
		"""
		DEBUG_MSG("%s::removeEnemy: %i entity=%i" % \
						(self.getScriptName(), self.id, entityID))
		
		self.enemyLog.remove(entityID)
		self.onRemoveEnemy(entityID)

	def onRemoveEnemy(self, entityID):
		"""
		virtual method.
		删除敌人
		"""
		pass
	
	def checkInTerritory(self):
		"""
		virtual method.
		检查自己是否在可活动领地中
		"""
		return True

	def checkEnemyDist(self, entity):
		"""
		virtual method.
		检查敌人距离
		"""
		dist = entity.position.distTo(self.position) <= 30.0
		if dist > 30.0:
			INFO_MSG("%s::checkEnemyDist: %i id=%i, dist=%f." % (self.getScriptName(), self.id, entity.id, dist))
			return False
		
		return True
		
	def checkEnemys(self):
		"""
		检查敌人列表
		"""
		for idx in range(len(self.enemyLog) - 1, -1, -1):
			entity = KBEngine.entities.get(self.enemyLog[idx])
			if entity is None or entity.isDestroyed or entity.isDead() or \
				not self.checkInTerritory() or not self.checkEnemyDist(entity):
				self.removeEnemy(self.enemyLog[idx])

Combat._timermap = {}
