# -*- coding: utf-8 -*-
import KBEngine
import GlobalConst
from KBEDebug import * 

class BAGITEM_INFOS(list):
	"""
	"""
	def __init__(self):
		"""
		"""
		list.__init__(self)
		
	def asDict(self):
		data = {
			"serialnum"			: self[0],
			"tableId"			: self[1],
			"bagFrameIndex"		: self[2],
			"bagItemIndex"		: self[3],
			"count"				: self[4],
			"name"				: self[5],
			"altnasIndex"		: self[6],
			"icon"				: self[7],
			"modelId"			: self[8],
			"stdMode"			: self[9],
			"weight"			: self[10],
			"func"			: self[11],
			"PA1"			: self[12],
			"PA2"			: self[13],
			"MA1"			: self[14],
			"MA2"			: self[15],
			"PD1"			: self[16],
			"PD2"			: self[17],
			"MD1"			: self[18],
			"MD2"			: self[19],
			"HP"			: self[20],
			"MP"			: self[21],
			"Accurate"			: self[22],
			"lucky"			: self[23],
			"holiness"			: self[24],
			"HPgen"			: self[25],
			"MPgen"			: self[26],
			"crit"			: self[27],
			"need"			: self[28],
			"needLevel"			: self[29],
			"price"			: self[30],
			"stock"			: self[31],
			"script"			: self[32],
		}
		
		return data

	def createFromDict(self, dictData):
		self.extend([dictData["serialnum"], dictData["tableId"], dictData["bagFrameIndex"], dictData["bagItemIndex"], dictData["count"], dictData["name"], dictData["altnasIndex"], dictData["icon"], dictData["modelId"], dictData["stdMode"], dictData["weight"], dictData["func"], dictData["PA1"], dictData["PA2"], dictData["MA1"], dictData["MA2"], dictData["PD1"], dictData["PD2"], dictData["MD1"], dictData["MD2"], dictData["HP"], dictData["MP"], dictData["Accurate"], dictData["lucky"], dictData["holiness"], dictData["HPgen"], dictData["MPgen"], dictData["crit"], dictData["need"], dictData["needLevel"], dictData["price"], dictData["stock"], dictData["script"]])
		return self
		
class BAGITEM_PICKLER:
	def __init__(self):
		pass

	def createObjFromDict(self, dct):
		return BAGITEM_INFOS().createFromDict(dct)

	def getDictFromObj(self, obj):
		return obj.asDict()

	def isSameType(self, obj):
		return isinstance(obj, BAGITEM_INFOS)

inst = BAGITEM_PICKLER()