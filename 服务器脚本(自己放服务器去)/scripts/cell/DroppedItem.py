# -*- coding: utf-8 -*-
import random
import math
import time
import SCDefine
import d_spaces
import KBEngine
from KBEDebug import *
from interfaces.GameObject import GameObject

class DroppedItem(KBEngine.Entity, GameObject):
	DESTROY_TIMER = 101
	def __init__(self):
		KBEngine.Entity.__init__(self)
		GameObject.__init__(self)
		
	def pickUpRequest(self, whomID):
		if self.pickerID == 0:
			picker = KBEngine.entities[whomID]
			picker.base.pickUpResponse(self.id, self.bagItemData )
			#self.addTimer( 2, 0, DroppedItem.DESTROY_TIMER )
			self.pickerID = whomID

	def onDestroyTimer(self, tid, tno):
		if ( tno == DroppedItem.DESTROY_TIMER ):
			self.destroy()
			
	def isPicked(self, success):
		if success == 0:
			self.pickerID = 0
			return
		if success == 1:
			self.destroy()
			return
			
DroppedItem._timermap = {}
DroppedItem._timermap[DroppedItem.DESTROY_TIMER] = DroppedItem.onDestroyTimer
DroppedItem._timermap.update(GameObject._timermap)