#pragma once

#include "IFramework.h"
#include "ErrorType.h"

namespace S = ::System;

namespace Adapter {

struct E_RESULT { enum TYPE {
	SUCCESS		= 0,
	FAILED,
}; /* TYPE */ }; /* E_RESULT */

struct E_ITEMPROPERTY { enum TYPE {
	 ACCESSORY,
	 BODY,
	 HEAD,
	 SHIELD,
	 WEAPON,
}; /* TYPE */ }; /* E_ITEMPROPERTY */


static const unsigned int s_dwMaxUserIDLength = 32;
static const unsigned int s_dwMaxUserPWDLength = 32;
static const unsigned int s_dwMaxNickNameLength = 32;

static const unsigned int s_dwMaxPacketSize = 1000;


//#define IS_NULL(p)	(!(p))
//#define IS_NOT_NULL(p)  (p)

// Packet String Size 정의
#define MAX_LEN_PK_NICK					25
#define MAX_LEN_PK_ID					32
#define MAX_LEN_PK_PW					32

////////////////////////////////////////////////////////
typedef S::WString<MAX_LEN_ID>		WSTR_ID;
typedef const WSTR_ID				CWSTR_ID;

////////////////////////////////////////////////////////
typedef S::WString<MAX_LEN_PW>		WSTR_PW;
typedef const WSTR_PW				CWSTR_PW;


// Passive Type
struct E_PASSIVE {
	enum TYPE {
		NONE = 0,
		HP_UP,		//< 체력 n%이하일 경우 체력 n% 회복
		DEFENCE_UP, //< 적에게 피격시 방어력 증가
		DAMAGE_UP,  //< 공속 증가시 공격력 증가


		COOLTIME_DECREASE = 1,	//< cooltime - 10%
		MAX_PASSIVE,
	}; /* TYPE */
}; /* E_BUY_UTILITY */


// Skill Type
struct E_SKILL_COND{
	enum TYPE {
		NONE = 0,
		COUNT,				//< 카운트 스킬
		REF_COUNT,			//< 누적 스킬
		STACK,				//< 스택 스킬
		CHARGE,				//< 차지 스킬
		CASTING,			//< 캐스팅(시전)스킬
		ONOFF,				//< ON/OFF 스킬
		SELECT_BULLET,		//< 불렛 선택 스킬
		MARKING,			//< 표식 스킬
		GLOBAL_ATTACK,		//< 글로벌 공격 스킬
		COMBO_DEBUF,		//< 콤보 디버프 스킬
		COMBO,				//< 콤보 스 킬
		DASH,				//< 대쉬 스킬
		JUMP,				//< 점프 스킬
		WARP,				//< 순간이동(워프) 스킬
		REMEMBER_POS,		//< 위치저장 이동 스킬
		GROUP,				//< 그룹형 스킬

		MAX_SKILL_COND_TYPE,
	};
};

#define RESUT_SUCCESS 0
#define	REDIS_EXPIRE_PERIOD 60
#define MAX(A, B) ((A) > (B) ? (A) : (B) )
#define MIN(A, B) ((A) < (B) ? (A) : (B) )

//#define CHECK_INFINITE_VARIABLE			int nWhileCount = 0
//#define CHECK_INFINITE_VARIABLE_INIT		nWhileCount = 0
//#define CHECK_INFINITE_WHILE				++nWhileCount; if( nWhileCount > 20000) {ASSERT_CRASH(false);}
//#define LOG_RECORD

#define WIN32_LEAN_AND_MEAN

#define GMTOOL_HOST	"localhost"
#define GMTOOL_PORT "9050"

#define COUPON_HOST "ts-coupon-api.four33.com"
#define COUPON_PORT "10071"

#define GOOGLE_BILLING_POST_URL			 "/oauth2/v4/token"
#define GOOGLE_BILLING_HOST_URL			 "www.googleapis.com"
#define GOOGLE_BILLING_GRANT_TYPE		 "grant_type=urn%3Aietf%3Aparams%3Aoauth%3Agrant-type%3Ajwt-bearer&assertion="


//#define DISPATCHER_NEW

} /* Adapter */
