(function (cjs, an) {

var p; // shortcut to reference prototypes
var lib={};var ss={};var img={};
lib.ssMetadata = [];


(lib.AnMovieClip = function(){
	this.currentSoundStreamInMovieclip;
	this.actionFrames = [];
	this.soundStreamDuration = new Map();
	this.streamSoundSymbolsList = [];

	this.gotoAndPlayForStreamSoundSync = function(positionOrLabel){
		cjs.MovieClip.prototype.gotoAndPlay.call(this,positionOrLabel);
	}
	this.gotoAndPlay = function(positionOrLabel){
		this.clearAllSoundStreams();
		this.startStreamSoundsForTargetedFrame(positionOrLabel);
		cjs.MovieClip.prototype.gotoAndPlay.call(this,positionOrLabel);
	}
	this.play = function(){
		this.clearAllSoundStreams();
		this.startStreamSoundsForTargetedFrame(this.currentFrame);
		cjs.MovieClip.prototype.play.call(this);
	}
	this.gotoAndStop = function(positionOrLabel){
		cjs.MovieClip.prototype.gotoAndStop.call(this,positionOrLabel);
		this.clearAllSoundStreams();
	}
	this.stop = function(){
		cjs.MovieClip.prototype.stop.call(this);
		this.clearAllSoundStreams();
	}
	this.startStreamSoundsForTargetedFrame = function(targetFrame){
		for(var index=0; index<this.streamSoundSymbolsList.length; index++){
			if(index <= targetFrame && this.streamSoundSymbolsList[index] != undefined){
				for(var i=0; i<this.streamSoundSymbolsList[index].length; i++){
					var sound = this.streamSoundSymbolsList[index][i];
					if(sound.endFrame > targetFrame){
						var targetPosition = Math.abs((((targetFrame - sound.startFrame)/lib.properties.fps) * 1000));
						var instance = playSound(sound.id);
						var remainingLoop = 0;
						if(sound.offset){
							targetPosition = targetPosition + sound.offset;
						}
						else if(sound.loop > 1){
							var loop = targetPosition /instance.duration;
							remainingLoop = Math.floor(sound.loop - loop);
							if(targetPosition == 0){ remainingLoop -= 1; }
							targetPosition = targetPosition % instance.duration;
						}
						instance.loop = remainingLoop;
						instance.position = Math.round(targetPosition);
						this.InsertIntoSoundStreamData(instance, sound.startFrame, sound.endFrame, sound.loop , sound.offset);
					}
				}
			}
		}
	}
	this.InsertIntoSoundStreamData = function(soundInstance, startIndex, endIndex, loopValue, offsetValue){ 
 		this.soundStreamDuration.set({instance:soundInstance}, {start: startIndex, end:endIndex, loop:loopValue, offset:offsetValue});
	}
	this.clearAllSoundStreams = function(){
		var keys = this.soundStreamDuration.keys();
		for(var i = 0;i<this.soundStreamDuration.size; i++){
			var key = keys.next().value;
			key.instance.stop();
		}
 		this.soundStreamDuration.clear();
		this.currentSoundStreamInMovieclip = undefined;
	}
	this.stopSoundStreams = function(currentFrame){
		if(this.soundStreamDuration.size > 0){
			var keys = this.soundStreamDuration.keys();
			for(var i = 0; i< this.soundStreamDuration.size ; i++){
				var key = keys.next().value; 
				var value = this.soundStreamDuration.get(key);
				if((value.end) == currentFrame){
					key.instance.stop();
					if(this.currentSoundStreamInMovieclip == key) { this.currentSoundStreamInMovieclip = undefined; }
					this.soundStreamDuration.delete(key);
				}
			}
		}
	}

	this.computeCurrentSoundStreamInstance = function(currentFrame){
		if(this.currentSoundStreamInMovieclip == undefined){
			if(this.soundStreamDuration.size > 0){
				var keys = this.soundStreamDuration.keys();
				var maxDuration = 0;
				for(var i=0;i<this.soundStreamDuration.size;i++){
					var key = keys.next().value;
					var value = this.soundStreamDuration.get(key);
					if(value.end > maxDuration){
						maxDuration = value.end;
						this.currentSoundStreamInMovieclip = key;
					}
				}
			}
		}
	}
	this.getDesiredFrame = function(currentFrame, calculatedDesiredFrame){
		for(var frameIndex in this.actionFrames){
			if((frameIndex > currentFrame) && (frameIndex < calculatedDesiredFrame)){
				return frameIndex;
			}
		}
		return calculatedDesiredFrame;
	}

	this.syncStreamSounds = function(){
		this.stopSoundStreams(this.currentFrame);
		this.computeCurrentSoundStreamInstance(this.currentFrame);
		if(this.currentSoundStreamInMovieclip != undefined){
			var soundInstance = this.currentSoundStreamInMovieclip.instance;
			if(soundInstance.position != 0){
				var soundValue = this.soundStreamDuration.get(this.currentSoundStreamInMovieclip);
				var soundPosition = (soundValue.offset?(soundInstance.position - soundValue.offset): soundInstance.position);
				var calculatedDesiredFrame = (soundValue.start)+((soundPosition/1000) * lib.properties.fps);
				if(soundValue.loop > 1){
					calculatedDesiredFrame +=(((((soundValue.loop - soundInstance.loop -1)*soundInstance.duration)) / 1000) * lib.properties.fps);
				}
				calculatedDesiredFrame = Math.floor(calculatedDesiredFrame);
				var deltaFrame = calculatedDesiredFrame - this.currentFrame;
				if(deltaFrame >= 2){
					this.gotoAndPlayForStreamSoundSync(this.getDesiredFrame(this.currentFrame,calculatedDesiredFrame));
				}
			}
		}
	}
}).prototype = p = new cjs.MovieClip();
// symbols:



(lib.constructNum = function() {
	this.initialize(img.constructNum);
}).prototype = p = new cjs.Bitmap();
p.nominalBounds = new cjs.Rectangle(0,0,1280,960);


(lib.spirtovka = function(mode,startPosition,loop) {
	this.initialize(mode,startPosition,loop,{});

	// timeline functions:
	this.frame_2 = function() {
		playSound("Спиртовка");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f("rgba(255,255,0,0.4)").s().p("AB9JtQgcgFg5gFQgkgDgWgFIhGgQIgqgKQghgKg/gaIgpgQIhHgeQgUgJgHgFIgUgPQgUgPgTgJQgXgJgGgFQgIgGgRgYQgOgVgOgKQgUgOgJgHQgUgPgMgXIgBgCQgHgDgFgHQgNgSgDgXQAAgLgBgFIgDgPQgBgKAAgKIABgrIABgGQgCgFgBgGQgBgPAHgOIAKgRQACgKgBgIQgCgJgNgOQgLgMgFgJQgEgKAAgSQgBgOAGg+QABgUADgJQAGgTASgPQAJgIAZgPIAAAAIAAgFQgCgeADgUQAFgfAegtQAdgtAbgRQAOgKAYgJQAsgSBRgTIAZgFIBnABIAhgBIAFAAIABgFQACgGgBgLQgEg4AVgpQANgZARgFIATgDQAMgCAGgDQAIgEAIgNQAJgPAFgEQALgKAVgEQArgIBFABQAYAAAMACIAeAIIAgAJQAYAHAGADQALAFARAKQAoAWAQAVQANATgDAPQgBAHgHAKIgJARQgDAIAAASIgDBRQAAATAEAJQAFALAOAHQAIAEATAHQArAPBJAoQAiARARANQAJAHATARQARAPALAHQAOAKAEAGQADAFABAGQAAAGgDAEQgBAFgGAGQgEAEgMAFQgUAMgRAaQgEAGgXApIgWAjQgNAXgHAPQgUAsAAAwIAAAHIAAACIAAASQgCAdAJAyQAJAvALAkQAEASAHAQQAGAMATAaQAPAWALAVQAFANAEAFIAIAJQADAGAAAGQAAAFgFAFQgFADgFAAQgbAmgTAOQgJAIgWANQgcAQgNAFIgfAJIhNAVIgeAGQgHACgbAAIh+gBIgDABIgGAAQgGAAgIgCg");
	this.shape.setTransform(-110.8521,2.4419);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f().s("#000000").ss(1,1,1).p("AF/AAQAACehwBxQhxBwieAAQieAAhwhwQhwhxAAieQAAidBwhxQBwhwCeAAQCeAABxBwQBwBxAACdg");
	this.shape_1.setTransform(-21.55,20.6);

	this.shape_2 = new cjs.Shape();
	this.shape_2.graphics.f("#FFFF00").s().p("AkOEOQhvhwAAieQAAidBvhxQBwhwCeAAQCeAABwBwQBwBxABCdQgBCehwBwQhwBxieAAQieAAhwhxg");
	this.shape_2.setTransform(-21.55,20.6);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape}]},1).to({state:[]},1).to({state:[{t:this.shape_2},{t:this.shape_1}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-173,-59.8,190.7,124.6);


(lib.shtativ_dlya_probirok = function(mode,startPosition,loop) {
	this.initialize(mode,startPosition,loop,{});

	// timeline functions:
	this.frame_2 = function() {
		playSound("Штативдляпробирок");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f("rgba(255,255,0,0.4)").s().p("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");
	this.shape.setTransform(-33.7595,-43.5064);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f().s("#000000").ss(1,1,1).p("AF/AAQAACehwBxQhxBwieAAQieAAhwhwQhwhxAAieQAAidBwhxQBwhwCeAAQCeAABxBwQBwBxAACdg");
	this.shape_1.setTransform(-21.55,20.6);

	this.shape_2 = new cjs.Shape();
	this.shape_2.graphics.f("#FFFF00").s().p("AkOEOQhvhwAAieQAAidBvhxQBwhwCeAAQCeAABwBwQBwBxABCdQgBCehwBwQhwBxieAAQieAAhwhxg");
	this.shape_2.setTransform(-21.55,20.6);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape}]},1).to({state:[]},1).to({state:[{t:this.shape_2},{t:this.shape_1}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-134.5,-179.4,201.5,271.9);


(lib.shtativ = function(mode,startPosition,loop) {
	this.initialize(mode,startPosition,loop,{});

	// timeline functions:
	this.frame_2 = function() {
		playSound("Лабораторныйштатив");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f("rgba(255,255,0,0.4)").s().p("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GgnABgUQADgcgBgsIgBhJQABgiAGhGQAHhFAAgjIABhAQAAgmAEgaQAHglAAgTIgBgbQgCgFAAgGIABgKIADgLIAAgXIACgVIACgVIgBgbQABgHADgMIADgTQABgIAAgOIAAgXIACgMIgCg4QAAgOACgEIACgEQgBgGACgKQAPhYACh/IADiGQABhRAEg1IABgJQgBgGABgIQALh0AEhGQAHhlgBhUIgBhNQACgkAJhGIABgJIAAgBQgFgEgBgGQAAgGADgSQACgMAAgaIACh6IAEg+IADguQABgPAEgGQACgEAFgDQABgDADgDQAFgEANAAIBhACIARgBIANgDQAHgBAFADIADACIAGABQAGADACAFQACAFgBALQgGAzABBkQACBmgGAxIgGAxIgCAxIAAAFQABAFAAAJQgCAeABBfQAAAbgCANQgCAXgJAtIgFApIgGAcQAAAIADARQAEARgBAIQgBAMgHAGIgBABIAAAFIgUDcQgCAYABAMQAAAQAEAeQAEAmgHBAQgIBLAAAbIABArQABAagDARQgBAJgEAGIAAAAQgGASgBAtQgBBigNBZQgGAmgBAOQgCAeAEAWQADARAAAGIgDARQgBAKABAVQACAtgGA5QgDAjgLBCIgBAFQADAGACALQAGArgEA1QgCAkgJA8IgFAfQADAYAAAeQAAAegCAQIgEAcIgDAbIAAATIgBASQgBAKgFATQgCANACAPQABATAGAUIABABIAPAEQAJACADAEQAFAGgCALQgBAFgGANQgIASgBAkIgFBuQAAAJgBAGQAgABAQACIAGglQAEgYAFgLIAFgNIADgJIAEgHIAFgKQACgGADgDQADgDARgFIARgFQAJgCAIADIAEADQAcgBAMAIQALAGAMAWQALAWABANQACAWgOAdQgaA2gwAjQAUgEANAOQAHAIAGAOIAJAZQAjACA/gHQAagDALgHQAIgFAIgKIAMgTQAPgYAWgXQAggkAfgJQAUgFATAFQAVAFALAPQAKAOABAUQAAARgIARQgLAYgjAeQgVASgDANQgCAIADAQIAHAmQAGAbgCAQQgEAZgWATQgUAQgcAIQgYAGgSgDQgYgFgKgSQgIgNAAgdQABgegGgNQgLgVgcgIQgQgEghABQgUAAgIAHQgHAFgHANQgHAPgFAFQgEAFgFACQAiAHAgARQATALASAPQAwAnAZA6QAGAPAOAuQANAtgBAYQgBAQgIAgQgGAWgKAFQgEACgIABIgMADQgGACgFAJIgJAOQgKALgcAIQgRAFgNAAQgKAAgIgEg");
	this.shape.setTransform(-27.514,-189.9012);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f().s("#000000").ss(1,1,1).p("AF/AAQAACehwBxQhxBwieAAQieAAhwhwQhwhxAAieQAAidBwhxQBwhwCeAAQCeAABxBwQBwBxAACdg");
	this.shape_1.setTransform(-21.55,20.6);

	this.shape_2 = new cjs.Shape();
	this.shape_2.graphics.f("#FFFF00").s().p("AkOEOQhvhwAAieQAAidBvhxQBwhwCeAAQCeAABwBwQBwBxABCdQgBCehwBwQhwBxieAAQieAAhwhxg");
	this.shape_2.setTransform(-21.55,20.6);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape}]},1).to({state:[]},1).to({state:[{t:this.shape_2},{t:this.shape_1}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-179.3,-520,303.6,660.2);


(lib.probirka = function(mode,startPosition,loop) {
	this.initialize(mode,startPosition,loop,{});

	// timeline functions:
	this.frame_2 = function() {
		playSound("Пробирка");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f("rgba(255,255,0,0.4)").s().p("AesFJQgPgBgEgCQgGgCgDgEQgEgEgBgFIAAgCIAAgEQABgDACgDIAAgCIABgDIACgDQABgFADgHQAXgzAIg5QAEgYgBgQQAAgcgLgaQgHgQADgHQACgIAIgCIAGAAIAIgBQBCgGA5AjQApAZAJAhQAEANAAAVQABAzgPAdIgNAUQgUAagdAQQgdARghAFQgLACgaAAIgWgBgAT/EGIg4gFIiYgQQhXgHhIgEQhqgGgigEQgrgHgWgCQgcgChGABQgtABgcgDIgrgGQgdgDhFABIgFAAQgIABgOgCQgkgFgrgDIg6gFQg+gEgVAAQgUgBgGgFQgLgHgDgZIgHgrQgBgJgDgDQgEgFgJgCQgjgKgXAJIgKAFIgGABIgGADQgKAGgQAYQgQAWgNAFQgHADgPAAQhLABglgEQgKgBgFgDIgBAAIgkABQguABgWgCIg3gFIiZgSQgTgCgHgGQgHgGgDgLIgDgTQgEgrgUgqQgGgMgHgDQgCgBgLAAIgUABQgLABgFADQgGACgIAKQgqAygpAKQgOADgmAAQghABgRAHQABAGgFAEQgFAFgGgBQgFAAgDgEQgGABgHgCQgqgGhHABQhVAAgcgCIg7gGIg7gGQgVgChKgEIiygHQgLgBgEgDIgBgBIgEgBIiSgPQgqgFgigCQgpgCg6AAIiZAAQhXgChCgFQgUgCgDgJQgBgFADgEQADgEAEgCIAGgMQATgiAPg9QAJgpAEgcQAHg7gLgsQgDgOADgGQAEgGAJgBQAGAAAFAEIACAAQAGAAAPAFQAVAIAlAFIBnAPQA5AHAvABIA5ABIA5ABIBXAGQA0AEAjgDQAQgBAGACIAEACIAEAAQAGAAALADQAOADAdgBIAjAEQAiAFBcAGICaAKQALAAAGADIAGADQAIAAAFACQAFACADADIAmADQArACAtAFQAkAFAOAAQAcACAVgEQANgDAGACIAEADQAHgBALABIBuAQQAlAFAQABIA4ABQAhAAAWAEIADABQAFgCAGAAQAPgBAUAMQAbASAGACQALAFAZAFQATAHAcAYQAPAMAGAIQAJAMAJAfIASA9QADAJAEADIAIABIAeADQAZACAMgCQAHgCACgDQACgDAAgJQAAgIAFgHQAFgHAIgDIARgFQAJgEAGgKQAJgRgCgYQAAgKgGghQgFgVAMgFQAHgDAHAFQAGgCAKAAQAYABAPADQAPADAdAIQATAEAZADQBGAJAhADQA0AEBJADQB1AFA6gFQANgBAGACQAHgCAKABQAKABATAEIAcAGQAQADAYABIAnACIAvAGQAdAEARABQARACAqgBQAmAAAVADIAxAHQAUADA2ABQBLACB1APIC/AXQATACADAKQABADAAADQACAFgCAHIgHAOQgNAVgHAoQgMA/ACAwQABATAFANIAGAMQADAHgBAFQgBAHgHAEIAAAAIgBABIgBAAIgBAAQgEACgGAAIgJgBg");
	this.shape.setTransform(-183.4844,5.8143);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f().s("#000000").ss(1,1,1).p("AFEAAQAACGhfBfQhfBgiGAAQiFAAhfhgQhfhfAAiGQAAiFBfhfQBfhgCFAAQCGAABfBgQBfBfAACFg");
	this.shape_1.setTransform(-21.575,20.625);

	this.shape_2 = new cjs.Shape();
	this.shape_2.graphics.f("#FFFF00").s().p("AjkDlQhfhfAAiGQAAiFBfhfQBfhgCFAAQCGAABfBgQBfBfAACFQAACGhfBfQhfBgiGAAQiFAAhfhgg");
	this.shape_2.setTransform(-21.575,20.625);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape}]},1).to({state:[]},1).to({state:[{t:this.shape_2},{t:this.shape_1}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-399.6,-27.1,432.20000000000005,81.2);


(lib.gazootvodnaya_trubka = function(mode,startPosition,loop) {
	this.initialize(mode,startPosition,loop,{});

	// timeline functions:
	this.frame_2 = function() {
		playSound("Газоотводнаятрубка");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f("rgba(255,255,0,0.4)").s().p("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");
	this.shape.setTransform(-319.6,264.7179);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f().s("#000000").ss(1,1,1).p("AF/AAQAACehwBxQhxBwieAAQieAAhwhwQhwhxAAieQAAidBwhxQBwhwCeAAQCeAABxBwQBwBxAACdg");
	this.shape_1.setTransform(-21.55,20.6);

	this.shape_2 = new cjs.Shape();
	this.shape_2.graphics.f("#FFFF00").s().p("AkOEOQhvhwAAieQAAidBvhxQBwhwCeAAQCeAABwBwQBwBxABCdQgBCehwBwQhwBxieAAQieAAhwhxg");
	this.shape_2.setTransform(-21.55,20.6);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape}]},1).to({state:[]},1).to({state:[{t:this.shape_2},{t:this.shape_1}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-746.3,-18.7,853.5,517.2);


// stage content:
(lib.StructureMapCopy_HTML5Canvas = function(mode,startPosition,loop) {
	this.initialize(mode,startPosition,loop,{});

	this.actionFrames = [0];
	this.isSingleFrame = false;
	// timeline functions:
	this.frame_0 = function() {
		if(this.isSingleFrame) {
			return;
		}
		if(this.totalFrames == 1) {
			this.isSingleFrame = true;
		}
		this.clearAllSoundStreams();
		 
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).call(this.frame_0).wait(1));

	// Слой_1
	this.instance = new lib.shtativ_dlya_probirok();
	this.instance.setTransform(271.7,503.35);
	new cjs.ButtonHelper(this.instance, 0, 1, 2, false, new lib.shtativ_dlya_probirok(), 3);

	this.instance_1 = new lib.gazootvodnaya_trubka();
	this.instance_1.setTransform(588.55,267.85,0.6632,0.6632);
	new cjs.ButtonHelper(this.instance_1, 0, 1, 2, false, new lib.gazootvodnaya_trubka(), 3);

	this.instance_2 = new lib.spirtovka();
	this.instance_2.setTransform(1140.45,528.9,0.9448,0.9448,0,0,0,0,1.6);
	new cjs.ButtonHelper(this.instance_2, 0, 1, 2, false, new lib.spirtovka(), 3);

	this.instance_3 = new lib.probirka();
	this.instance_3.setTransform(1047.65,339.4);
	new cjs.ButtonHelper(this.instance_3, 0, 1, 2, false, new lib.probirka(), 3);

	this.instance_4 = new lib.shtativ();
	this.instance_4.setTransform(863.75,519);
	new cjs.ButtonHelper(this.instance_4, 0, 1, 2, false, new lib.shtativ(), 3);

	this.instance_5 = new lib.constructNum();
	this.instance_5.setTransform(0,-115);

	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(1,1,1).p("AC0iEQAAAWgQAPQgPAPgVAAQgWAAgPgPQgPgPAAgWQAAgVAPgQQAPgPAWAAQAVAAAPAPQAQAQAAAVgAieCKQAAAUgDANQgDAOgEAAQgFAAgDgOQgDgNAAgUQAAgTADgOQADgNAFAAQAEAAADANQADAOAAATg");
	this.shape.setTransform(1138.275,523.975);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#FFFF00").s().p("AiwCrQgDgNAAgUQAAgTADgOQADgNAFAAQAEAAADANQADAOAAATQAAAUgDANQgDAOgEAAQgFAAgDgOgABbhfQgPgPAAgWQAAgVAPgQQAPgPAWAAQAVAAAPAPQAQAQAAAVQAAAWgQAPQgPAPgVAAQgWAAgPgPg");
	this.shape_1.setTransform(1138.275,523.975);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.shape_1},{t:this.shape},{t:this.instance_5},{t:this.instance_4},{t:this.instance_3},{t:this.instance_2},{t:this.instance_1},{t:this.instance}]}).wait(1));

	this._renderFirstFrame();

}).prototype = p = new lib.AnMovieClip();
p.nominalBounds = new cjs.Rectangle(640,245,640,600);
// library properties:
lib.properties = {
	id: 'CA7F3C98344D1D46A3930D62D247FBB1',
	width: 1280,
	height: 720,
	fps: 30,
	color: "#FFFFFF",
	opacity: 1.00,
	manifest: [
		{src:"images/constructNum.jpg?1634481773869", id:"constructNum"},
		{src:"sounds/Газоотводнаятрубка_.mp3?1634481773869", id:"Газоотводнаятрубка"},
		{src:"sounds/Лабораторныйштатив_.mp3?1634481773869", id:"Лабораторныйштатив"},
		{src:"sounds/Пробирка_.mp3?1634481773869", id:"Пробирка"},
		{src:"sounds/Спиртовка_.mp3?1634481773869", id:"Спиртовка"},
		{src:"sounds/Штативдляпробирок_.mp3?1634481773869", id:"Штативдляпробирок"}
	],
	preloads: []
};



// bootstrap callback support:

(lib.Stage = function(canvas) {
	createjs.Stage.call(this, canvas);
}).prototype = p = new createjs.Stage();

p.setAutoPlay = function(autoPlay) {
	this.tickEnabled = autoPlay;
}
p.play = function() { this.tickEnabled = true; this.getChildAt(0).gotoAndPlay(this.getTimelinePosition()) }
p.stop = function(ms) { if(ms) this.seek(ms); this.tickEnabled = false; }
p.seek = function(ms) { this.tickEnabled = true; this.getChildAt(0).gotoAndStop(lib.properties.fps * ms / 1000); }
p.getDuration = function() { return this.getChildAt(0).totalFrames / lib.properties.fps * 1000; }

p.getTimelinePosition = function() { return this.getChildAt(0).currentFrame / lib.properties.fps * 1000; }

an.bootcompsLoaded = an.bootcompsLoaded || [];
if(!an.bootstrapListeners) {
	an.bootstrapListeners=[];
}

an.bootstrapCallback=function(fnCallback) {
	an.bootstrapListeners.push(fnCallback);
	if(an.bootcompsLoaded.length > 0) {
		for(var i=0; i<an.bootcompsLoaded.length; ++i) {
			fnCallback(an.bootcompsLoaded[i]);
		}
	}
};

an.compositions = an.compositions || {};
an.compositions['CA7F3C98344D1D46A3930D62D247FBB1'] = {
	getStage: function() { return exportRoot.stage; },
	getLibrary: function() { return lib; },
	getSpriteSheet: function() { return ss; },
	getImages: function() { return img; }
};

an.compositionLoaded = function(id) {
	an.bootcompsLoaded.push(id);
	for(var j=0; j<an.bootstrapListeners.length; j++) {
		an.bootstrapListeners[j](id);
	}
}

an.getComposition = function(id) {
	return an.compositions[id];
}


an.makeResponsive = function(isResp, respDim, isScale, scaleType, domContainers) {		
	var lastW, lastH, lastS=1;		
	window.addEventListener('resize', resizeCanvas);		
	resizeCanvas();		
	function resizeCanvas() {			
		var w = lib.properties.width, h = lib.properties.height;			
		var iw = window.innerWidth, ih=window.innerHeight;			
		var pRatio = window.devicePixelRatio || 1, xRatio=iw/w, yRatio=ih/h, sRatio=1;			
		if(isResp) {                
			if((respDim=='width'&&lastW==iw) || (respDim=='height'&&lastH==ih)) {                    
				sRatio = lastS;                
			}				
			else if(!isScale) {					
				if(iw<w || ih<h)						
					sRatio = Math.min(xRatio, yRatio);				
			}				
			else if(scaleType==1) {					
				sRatio = Math.min(xRatio, yRatio);				
			}				
			else if(scaleType==2) {					
				sRatio = Math.max(xRatio, yRatio);				
			}			
		}			
		domContainers[0].width = w * pRatio * sRatio;			
		domContainers[0].height = h * pRatio * sRatio;			
		domContainers.forEach(function(container) {				
			container.style.width = w * sRatio + 'px';				
			container.style.height = h * sRatio + 'px';			
		});			
		stage.scaleX = pRatio*sRatio;			
		stage.scaleY = pRatio*sRatio;			
		lastW = iw; lastH = ih; lastS = sRatio;            
		stage.tickOnUpdate = false;            
		stage.update();            
		stage.tickOnUpdate = true;		
	}
}
an.handleSoundStreamOnTick = function(event) {
	if(!event.paused){
		var stageChild = stage.getChildAt(0);
		if(!stageChild.paused){
			stageChild.syncStreamSounds();
		}
	}
}


})(createjs = createjs||{}, AdobeAn = AdobeAn||{});
var createjs, AdobeAn;