
module SmashLeague {
  'use strict';

  export function IsPlayerRoleActive() {
    return (field: number, role: string) => {
      switch (role.toLowerCase()) {
        case 'tank':
          return (field & 0x1) == 0x1;
        case 'assassin':
          return (field & 0x2) == 0x2;
        case 'support':
          return (field & 0x4) == 0x4;
        case 'specialist':
          return (field & 0x8) == 0x8;
      }
    }
  }

  Application.Module.filter('isPlayerRoleActive', IsPlayerRoleActive);
}