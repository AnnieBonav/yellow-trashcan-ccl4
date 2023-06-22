/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_ANGRYSOUND = 3825176718U;
        static const AkUniqueID PLAY_BACKGROUNDMUSIC = 548088167U;
        static const AkUniqueID PLAY_BOOKODIALOGUE = 2906353518U;
        static const AkUniqueID PLAY_BUBBLES = 3381697299U;
        static const AkUniqueID PLAY_CORRECTPOTION = 3886503793U;
        static const AkUniqueID PLAY_DOOR = 2547633870U;
        static const AkUniqueID PLAY_DROP = 2007351433U;
        static const AkUniqueID PLAY_FOOTSTEP = 1602358412U;
        static const AkUniqueID PLAY_HAPPYSOUND = 1832643493U;
        static const AkUniqueID PLAY_INCORRECTPOTION = 4198774158U;
        static const AkUniqueID PLAY_PICKINGREDIENT = 644243960U;
        static const AkUniqueID PLAY_REFILL = 4075562094U;
        static const AkUniqueID STOP_BUBBLES = 1962155989U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace CURRENTROOM
        {
            static const AkUniqueID GROUP = 1662371969U;

            namespace STATE
            {
                static const AkUniqueID BREWING = 2473152703U;
                static const AkUniqueID ENTRANCE = 2656882895U;
                static const AkUniqueID GARDEN = 278887670U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace CURRENTROOM

    } // namespace STATES

    namespace SWITCHES
    {
        namespace CHARACTER
        {
            static const AkUniqueID GROUP = 436743010U;

            namespace SWITCH
            {
                static const AkUniqueID FAIRY = 2469822380U;
                static const AkUniqueID SOLDIER = 3121013053U;
            } // namespace SWITCH
        } // namespace CHARACTER

        namespace INGREDIENT
        {
            static const AkUniqueID GROUP = 1746288848U;

            namespace SWITCH
            {
                static const AkUniqueID BARK = 1274655755U;
                static const AkUniqueID HERB = 3196610212U;
                static const AkUniqueID LIQUID = 4087983317U;
                static const AkUniqueID MUSHROOM = 1941802987U;
            } // namespace SWITCH
        } // namespace INGREDIENT

        namespace MATERIAL
        {
            static const AkUniqueID GROUP = 3865314626U;

            namespace SWITCH
            {
                static const AkUniqueID GLASS = 2449969375U;
                static const AkUniqueID METAL = 2473969246U;
                static const AkUniqueID SOLID = 744619566U;
            } // namespace SWITCH
        } // namespace MATERIAL

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID BUBBLESINTENSITY = 3064925121U;
        static const AkUniqueID VOLUMEMUSIC = 3923584592U;
        static const AkUniqueID VOLUMESFX = 399092848U;
        static const AkUniqueID VOLUMEUI = 2007845773U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID DEFAULT = 782826392U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
