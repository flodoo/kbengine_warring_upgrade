# -*- coding: utf-8 -*-
import KBEngine
import random
import SCDefine
import math
import time
import d_spaces
import d_avatar_inittab
import d_knapsackItems
from KBEDebug import *
from interfaces.GameObject import GameObject
from interfaces.Teleport import Teleport
from BAGITEM import BAGITEM_INFOS

class Avatar(KBEngine.Proxy,
			GameObject,
			Teleport):
	"""
	角色实体
	"""
	def __init__(self):
		KBEngine.Proxy.__init__(self)
		GameObject.__init__(self)
		Teleport.__init__(self)

		# 如果登录是一个副本, 无论如何登录都放置在主场景上
		spacedatas = d_spaces.datas [self.cellData["spaceUType"]]
		avatar_inittab = d_avatar_inittab.datas[self.roleType]

		if "Duplicate" in spacedatas["entityType"]:
			self.cellData["spaceUType"] = avatar_inittab["spaceUType"]
			self.cellData["direction"] = (0, 0, avatar_inittab["spawnYaw"])
			self.cellData["position"] = avatar_inittab["spawnPos"]
		
		self.accountEntity = None
		self.cellData["dbid"] = self.databaseID
		self.nameB = self.cellData["name"]
		self.spaceUTypeB = self.cellData["spaceUType"]
		
		self._destroyTimer = 0
		
		#测试用 zcg
		self.BagItemList = []
		self.EquipmentList = []
		if len(self.BagItemList) == 0:
			for i in range(4,5):
				item = BAGITEM_INFOS()
				self.maxItemsCount +=1
				temp = d_knapsackItems.allItems[i]
				item.extend([self.maxItemsCount, i, 0, i, 1, temp["name"], temp["altnasIndex"], temp["icon"], temp["modelId"], temp["stdMode"], temp["weight"], temp["func"], temp["PA1"], temp["PA2"], temp["MA1"], temp["MA2"], temp["PD1"], temp["PD2"], temp["MD1"], temp["MD2"], temp["HP"], temp["MP"], temp["Accurate"], temp["lucky"], temp["holiness"], temp["HPgen"], temp["MPgen"], temp["crit"], temp["need"], temp["needLevel"], temp["price"], temp["stock"], temp["script"]])
				self.BagItemList.append(item)

	def onEntitiesEnabled(self):
		"""
		KBEngine method.
		该entity被正式激活为可使用， 此时entity已经建立了client对应实体， 可以在此创建它的
		cell部分。
		"""
		INFO_MSG("Avatar[%i-%s] entities enable. spaceUTypeB=%s, mailbox:%s" % (self.id, self.nameB, self.spaceUTypeB, self.client))
		
		if hasattr(self, "cellData"):
			# 防止使用统一个号登陆不同的demo造成无法找到匹配的地图从而无法加载资源导致无法进入游戏
			# 这里检查一下， 发现不对则强制同步到匹配的地图
			if self.getClientType() == 2:
				self.cellData["spaceUType"] = 2
				spacedatas = d_spaces.datas [self.cellData["spaceUType"]]
				self.cellData["position"] = spacedatas.get("spawnPos", (0,0,0))
			elif self.getClientType() == 5 or self.getClientType() == 1:
				if self.cellData["spaceUType"] == 1 or self.cellData["spaceUType"] == 2:
					self.cellData["spaceUType"] = 3
					spacedatas = d_spaces.datas [self.cellData["spaceUType"]]
					self.cellData["position"] = spacedatas.get("spawnPos", (0,0,0))
			else:
				self.cellData["spaceUType"] = 1
				spacedatas = d_spaces.datas [self.cellData["spaceUType"]]
				self.cellData["position"] = spacedatas.get("spawnPos", (0,0,0))
			
			self.spaceUTypeB = self.cellData["spaceUType"]
		
		KBEngine.globalData["Spaces"].loginToSpace(self, self.spaceUTypeB, {})
		
	def onGetCell(self):
		"""
		KBEngine method.
		entity的cell部分实体被创建成功
		"""
		self.itemInfo = 0
		for item in self.BagItemList:
			self.client.onAddBagItem(item)
			self.itemInfo = self.itemInfo | (1<<item[3])
		
		#FIXME:以后会发列表
		self.EquipmentList = []
		if len(self.EquipmentList) > 0:
			for equip in self.EquipmentList:
				self.client.onEquiptedItem(equip)
				self.cell.EquipNotify(equip)
		DEBUG_MSG('Avatar::onGetCell: %s' % self.cell)
		
	def createCell(self, space):
		"""
		defined method.
		创建cell实体
		"""
		self.createCellEntity(space)
	
	def destroySelf(self):
		"""
		"""
		if self.client is not None:
			return
			
		if self.cell is not None:
			# 销毁cell实体
			self.destroyCellEntity()
			return
			
		# 如果帐号ENTITY存在 则也通知销毁它
		if self.accountEntity != None:
			if time.time() - self.accountEntity.relogin > 1:
				self.accountEntity.activeCharacter = None
				self.accountEntity.destroy()
				self.accountEntity = None
			else:
				DEBUG_MSG("Avatar[%i].destroySelf: relogin =%i" % (self.id, time.time() - self.accountEntity.relogin))
				
		# 销毁base
		self.destroy()

	def onClientDeath(self):
		"""
		KBEngine method.
		entity丢失了客户端实体
		"""
		DEBUG_MSG("Avatar[%i].onClientDeath:" % self.id)
		# 防止正在请求创建cell的同时客户端断开了， 我们延时一段时间来执行销毁cell直到销毁base
		# 这段时间内客户端短连接登录则会激活entity
		self._destroyTimer = self.addTimer(1, 0, SCDefine.TIMER_TYPE_DESTROY)
			
	def onClientGetCell(self):
		"""
		KBEngine method.
		客户端已经获得了cell部分实体的相关数据
		"""
		INFO_MSG("Avatar[%i].onClientGetCell:%s" % (self.id, self.client))
		
	def onDestroyTimer(self, tid, tno):
		DEBUG_MSG("Avatar::onTimer: %i, tid:%i, arg:%i" % (self.id, tid, tno))
		self.destroySelf()
		
	def setBagItemPos(self, serialnum, FrameIndex, BagIndex):
		DEBUG_MSG("Avatar::setBagItemPos: ")
		for item in self.BagItemList:
			#data = item.asDict()
			#if data["serialnum"] == serialnum:
			#	data["bagFrameIndex"] = FrameIndex
			#	data["bagItemIndex"] = BagIndex
			#	break
			if item[0] == serialnum:
				item[2] = FrameIndex
				item[3] = BagIndex
				break

	def rePlaceBagItemPos(self, new_serialnum, old_serialnum):
		DEBUG_MSG("Avatar::rePlaceBagItemPos: ")
		isFindnew = False
		isFindold = False
		for item in self.BagItemList:
			if item[0] == new_serialnum:
				newItem = item
				isFindnew = True
			if item[0] == old_serialnum:
				oldItem = item
				isFindnew = True
			if isFindnew and isFindold:
				break
		if not isFindnew and isFindold:
			return
		new_frame = newItem[2]
		new_bagindex = newItem[3]
		old_frame = oldItem[2]
		old_bagindex = oldItem[3]
		newItem[2] = old_frame
		newItem[3] = old_bagindex
		oldItem[2] = new_frame
		oldItem[3] = new_bagindex
		
	def DropBagItem(self, dropped_serialnum):
		DEBUG_MSG("Avatar::rePlaceBagItemPos: ")
		droppedItem = None
		for item in self.BagItemList:
			if item[0] == dropped_serialnum:
				droppedItem = item
				break
		if droppedItem!=None:
			self.cell.dropNotify(droppedItem)
			self.BagItemList.remove(droppedItem)

	def pickUpResponse(self, itemId, bagItem):
		index = 0
		frameindex = 0
		if len(self.BagItemList) > 60:
			self.cell.pickUpResponse(0, itemId)
			return
		if len(self.BagItemList) > 0:
			for i in range(0,60):
				bit = (1<<i)&self.itemInfo
				if bit == 0:
					index = i
					self.itemInfo = self.itemInfo |(1<<i)
					break
			#sorted(self.BagItemList, key=lambda student : student[3])
			#item = self.BagItemList[0]
			#if item[3]>0:
			#	index = 0
			#else :
			#	for bitem in self.BagItemList:
			#		if bitem[3] == index:
			#			index = index + 1
			#		else:
			#			break
		else:
			self.itemInfo = 1;
		if bagItem[0] ==0:
			self.maxItemsCount +=1
			bagItem[0] = self.maxItemsCount
		newitem = BAGITEM_INFOS()
		newitem.extend([bagItem[0], bagItem[1], 0, index, 1, bagItem[5], bagItem[6], bagItem[7], bagItem[8], bagItem[9], bagItem[10], bagItem[11], bagItem[12], bagItem[13], bagItem[14], bagItem[15], bagItem[16], bagItem[17], bagItem[18], bagItem[19], bagItem[20], bagItem[21], bagItem[22], bagItem[23], bagItem[24], bagItem[25], bagItem[26], bagItem[27], bagItem[28], bagItem[29], bagItem[30], bagItem[31], bagItem[32]])
		self.BagItemList.append(newitem)
		self.cell.pickUpResponse(1, itemId)
		self.client.onAddBagItem(newitem)
	
	def ItemUse(self, serialnum):
		useItem = None
		for item in self.BagItemList:
			if item[0] == serialnum:
				useItem = item
				break
		if useItem!=None:
			self.cell.ItemUse(useItem)
			self.BagItemList.remove(useItem)
			mask = ~(1 << useItem[3])
			self.itemInfo = self.itemInfo &mask
			self.client.onDelBagItem(serialnum)
	
	def AddItem(self, bagItem):
		index = 0
		frameindex = 0
		if len(self.BagItemList) > 60:
			return
		if len(self.BagItemList) > 0:
			for i in range(0,60):
				bit = (1<<i)&self.itemInfo
				if bit == 0:
					index = i
					break
		if bagItem[0] ==0:
			self.maxItemsCount +=1
			bagItem[0] = self.maxItemsCount
		newitem = BAGITEM_INFOS()
		newitem.extend([bagItem[0], bagItem[1], 0, index, 1, bagItem[5], bagItem[6], bagItem[7], bagItem[8], bagItem[9], bagItem[10], bagItem[11], bagItem[12], bagItem[13], bagItem[14], bagItem[15], bagItem[16], bagItem[17], bagItem[18], bagItem[19], bagItem[20], bagItem[21], bagItem[22], bagItem[23], bagItem[24], bagItem[25], bagItem[26], bagItem[27], bagItem[28], bagItem[29], bagItem[30], bagItem[31], bagItem[32]])
		self.BagItemList.append(newitem)
		self.client.onAddBagItem(newitem)
	
	def EquipItem(self, serialnum, index):
		DEBUG_MSG("Avatar::EquipItem: ")
		equipItem = None
		for item in self.BagItemList:
			if item[0] == serialnum:
				equipItem = item
				break
		if equipItem!=None:
			self.BagItemList.remove(equipItem)
			mask = ~(1 << equipItem[3])
			self.itemInfo = self.itemInfo &mask
			equipItem[2] = index
			self.EquipmentList.append(equipItem)
			self.cell.EquipNotify(equipItem)
			self.client.onEquiptedItem(equipItem)
			
Avatar._timermap = {}
Avatar._timermap.update(GameObject._timermap)
Avatar._timermap.update(Teleport._timermap)
Avatar._timermap[SCDefine.TIMER_TYPE_DESTROY] = Avatar.onDestroyTimer

