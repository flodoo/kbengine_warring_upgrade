# -*- coding: utf-8 -*-
#
"""
"""
import KBEngine
from KBEDebug import *
import d_knapsackItems
from items import *
from items.NormalItem import *

_g_items = {}
def onInit():
	for key, datas in d_knapsackItems.allItems.items():
		script = datas['script']
		ERROR_MSG("script is =%s" % (script))
		if _g_items.get(script, None) != None:
			continue
		scriptinst = eval(script)()
		_g_items[script] = scriptinst
		#scriptinst.loadFromDict(datas)

def checkItemNo(itemNO):
	return itemNO in d_items

def noAlias2ItemNo(aid):
	return g_noAlias2ItemNo.get(aid, 0)

def itemNo2NoAlias(sid):
	return g_itemNo2NoAlias.get(sid, 0)

def getItemData(itemNO):
	"""
	获得物品的配置数据
	"""
	return d_items.get(itemNO, {})

def getItemClass(itemNO):
	"""
	获得物品的配置数据
	"""
	return d_items[itemNO]["script"]

def createItem(itemNO, amount = 1, owner = None):
	"""
	创建物品
	"""
	INFO_MSG("%i created. amount=%i" % (itemNO, amount))

	stackMax = getItemData(itemNO).get("overlayMax", 1)
	itemList = []
	while amount > 0:
		itemAmount = (amount < stackMax) and amount or stackMax
		item = getItemClass(itemNO)(itemNO, scutils.newUID(), itemAmount)
		item.onCreate(owner)
		itemList.append(item)
		amount -= itemAmount

	return itemList

def createItemByItem(item, amount, owner = None):
	"""
	根据一个已知物品来创建一个新的物品
	"""
	newItem = copy.deepcopy(item)
	newItem.setUUID(scutils.newUID())
	newItem.setAmount(amount)
	INFO_MSG("new item created. new item uuid=%i, src item uuid=%i" % (newItem.getUUID(), item.getUUID()))
	return newItem
	
def getitem(scriptname):
	return _g_items.get(scriptname, None)