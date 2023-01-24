var YandexSDK = {
	Authorization: function(scopes, photoSize) {
		authorization(
			scopes, 
			UTF8ToString(photoSize)
		);
	},
	
	InitPlayer: function(scopes) {
		initPlayer(scopes);
	},
	
	SetLeaderboardScore: function(score) {
		setLeaderboardScore(score);
	},
	
	ShowInterstitial: function() {
		showInterstitial();
	},
	
	ShowRewarded: function(id) {
		showRewarded(id);
	},

	IsMobile: function() {
		return Module.SystemInfo.mobile;
	} 
};

mergeInto(LibraryManager.library, YandexSDK);